using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Config.HubConfig
{
    public class StreamHub : Hub
    {
        public static Dictionary<string, List<UserAccountAuthRequest>> ConnectedClients = new Dictionary<string, List<UserAccountAuthRequest>>();
        public static Dictionary<string, List<LessonResponse>> Lessons = new Dictionary<string, List<LessonResponse>>();
        public static Dictionary<string, List<TestResultResponse>> TestResults = new Dictionary<string, List<TestResultResponse>>();
        public static Dictionary<string, OperationFlag> OperationFlags = new Dictionary<string, OperationFlag>();
        public static Dictionary<string, int> Time = new Dictionary<string, int>();
        public static Dictionary<string, QuestionResponse> Question = new Dictionary<string, QuestionResponse>();
        public static Dictionary<string, Timer> TimeInterval = new Dictionary<string, Timer>();

        private readonly ILessonService _lessonService;

        private readonly IQuestionService _questionService;

        private readonly ITestResultService _testResultService;

        public StreamHub(ILessonService lessonService, 
                        IQuestionService questionService,
                        ITestResultService testResultService)
        {
            this._lessonService = lessonService;
            this._questionService = questionService;
            this._testResultService = testResultService;
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public Task SendMessage(object message, string roomName, UserAccountAuthRequest userAccountAuthRequest)
        {
            EmitLog("Client " + Context.ConnectionId + " said: " + message, roomName);

            var time = DateTime.UtcNow.ToLocalTime().ToString();

            return Clients.OthersInGroup(roomName).SendAsync("message", message, userAccountAuthRequest, time);
        }

        public Task CreateOrJoinRoom(string roomName, UserAccountAuthRequest userAccountAuthRequest)
        {
            EmitLog("Received request to create or join room " + roomName + " from a client " + userAccountAuthRequest.DisplayName, roomName);

            if (!ConnectedClients.ContainsKey(roomName))
            {
                ConnectedClients.Add(roomName, new List<UserAccountAuthRequest>());
                Lessons.Add(roomName, new List<LessonResponse>());
                TestResults.Add(roomName, new List<TestResultResponse>());
                OperationFlags.Add(roomName, new OperationFlag());
                Time.Add(roomName, 0);
                Question.Add(roomName, new QuestionResponse());
                TimeInterval.Add(roomName, new Timer(_ => SetTime(roomName), null, 0, 1000));
                //TimeInterval.Add(roomName, new Timer(_ => Console.WriteLine("hello"), null, 0, 1000));
            }

            var user = ConnectedClients[roomName].Where(w => w.Id.Equals(userAccountAuthRequest.Id)).FirstOrDefault();

            if (!ConnectedClients[roomName].Contains(user))
            {
                ConnectedClients[roomName].Add(userAccountAuthRequest);
            }

            EmitJoinRoom(roomName);

            var numberOfClients = ConnectedClients[roomName].Count;

            if (numberOfClients == 1)
            {
                EmitCreated(roomName);
                EmitLog("Client " + Context.ConnectionId + " created the room " + roomName, roomName);
            }
            else
            {
                EmitJoined(roomName);
                EmitLog("Client " + Context.ConnectionId + " joined the room " + roomName, roomName);
            }

            EmitLog("Room " + roomName + " now has " + numberOfClients + " client(s)", roomName);

            return Task.Run(() => { return; });
        }

        public Task LeaveRoom(string roomName, UserAccountAuthRequest userAccountAuthRequest)
        {
            EmitLog("Received request to leave the room " + roomName + " from a client " + userAccountAuthRequest.DisplayName, roomName);

            var user = ConnectedClients[roomName].Where(w => w.Id.Equals(userAccountAuthRequest.Id)).FirstOrDefault();

            if (ConnectedClients.ContainsKey(roomName) && ConnectedClients[roomName].Contains(user))
            {
                ConnectedClients[roomName].Remove(user);
                EmitLeft(roomName);
                EmitLog("Client " + Context.ConnectionId + " left the room " + roomName, roomName);

                if (ConnectedClients[roomName].Count == 0)
                {
                    ConnectedClients.Remove(roomName);
                    Lessons.Remove(roomName);
                    TestResults.Remove(roomName);
                    OperationFlags.Remove(roomName);
                    Time.Remove(roomName);
                    Question.Remove(roomName);
                    TimeInterval[roomName].Change(Timeout.Infinite, Timeout.Infinite);
                    TimeInterval.Remove(roomName);
                    EmitLog("Room " + roomName + " is now empty - resetting its state", roomName);
                }
            }

            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task AddLesson(string roomName, Guid lessonId)
        {
            if (!ConnectedClients.ContainsKey(roomName))
            {
                await EmitLog("Room " + roomName + " is not found!", roomName);
            }
            
            var lesson = await this._lessonService.GetLessonById(lessonId);
            await EmitLog("Show lesson in the room " + roomName, roomName);
            Lessons[roomName].Add(lesson);

            await Clients.Group(roomName).SendAsync("lesson", Lessons[roomName].ToList());
        }

        public Task UpdateLesson(string roomName, bool isFront, Guid lessonId)
        {
            if (!ConnectedClients.ContainsKey(roomName))
            {
                EmitLog("Room " + roomName + " is not found!", roomName);
            }

            var lesson = Lessons[roomName].Where(w => w.Id.Equals(lessonId)).FirstOrDefault(p => p.IsFront = isFront);

            return Clients.Group(roomName).SendAsync("lesson", Lessons[roomName].ToList());
        }

        public Task RemoveLesson(string roomName, Guid lessonId)
        {
            if (!ConnectedClients.ContainsKey(roomName))
            {
                EmitLog("Room " + roomName + " is not found!", roomName).Wait();
            }

            var lesson = Lessons[roomName].Where(w => w.Id.Equals(lessonId)).FirstOrDefault();

            Lessons[roomName].Remove(lesson);

            return Clients.Group(roomName).SendAsync("lesson", Lessons[roomName].ToList());
        }

        public async Task AddQuestion(string roomName, Guid questionId)
        {
            if (!ConnectedClients.ContainsKey(roomName))
            {
                await EmitLog("Room " + roomName + " is not found!", roomName);
            }

            var question = await this._questionService.GetQuestionById(questionId);
            Question[roomName] = question;

            TestResults[roomName] = new List<TestResultResponse>();

            //add or update question for dictionary
            await Clients.Groups(roomName).SendAsync("test", new List<TestResultResponse>());
            await Clients.Group(roomName).SendAsync("question", question);
        }

        public async Task DoTest(string roomName, DoTestRequest doTestRequest)
        {
            var testResult = await this._testResultService.CalculateTestResult(doTestRequest);

            TestResults[roomName].Add(testResult);

            var user = ConnectedClients[roomName].Where(w => w.Id.Equals(doTestRequest.UserId)).FirstOrDefault();

            await Clients.Groups(roomName).SendAsync("test", TestResults[roomName].ToList());
        }

        public async Task ShowAnswerChoice(string roomName, bool isShowAnswerChoice)
        {
            if (!ConnectedClients.ContainsKey(roomName))
            {
                await EmitLog("Room " + roomName + " is not found!", roomName);
            }

            await Clients.Group(roomName).SendAsync("isShowAnswerChoice", isShowAnswerChoice);
        }

        public async Task ShowCorrectAnswer(string roomName, bool isShowCorrectAnswer)
        {
            if (!ConnectedClients.ContainsKey(roomName))
            {
                await EmitLog("Room " + roomName + " is not found!", roomName);
            }

            await Clients.Group(roomName).SendAsync("isShowCorrectAnswer", isShowCorrectAnswer);
        }

        public async Task InviteForPresenting(string roomName, UserAccountAuthRequest userEntry, bool isPresenting)
        {
            string Presenting = isPresenting ? "is presenting" : "stopped the presentation";
            await EmitLog("Room " + roomName + ", User: " + userEntry.DisplayName + " " + Presenting, roomName);

            await Clients.Group(roomName).SendAsync("presenting", userEntry, isPresenting);
        }

        public async Task SendPrivateMessage(string roomName, 
                                            UserAccountAuthRequest sendUserEntry, 
                                            UserAccountAuthRequest receiveUserEntry, 
                                            string message)
        {
            var time = DateTime.UtcNow.ToLocalTime().ToString();
            await Clients.Group(roomName).SendAsync("privateMessage", sendUserEntry, receiveUserEntry, message, time);
        }

        public async Task RaiseHand(string roomName, UserAccountAuthRequest sendUserEntry)
        {
            await Clients.Group(roomName).SendAsync("raiseHand", sendUserEntry);
        }

        public async Task SetOperationFlag(string roomName, OperationFlag operationFlag) {

            OperationFlags[roomName] = operationFlag;
            await Clients.Group(roomName).SendAsync("operationFlag", operationFlag);
        }

        public async Task GetOperationFlag(string roomName)
        {
            var operation = OperationFlags[roomName];
            if (operation.IsShowingLesson)
            {
                await Clients.Group(roomName).SendAsync("lesson", Lessons[roomName].ToList());
            }
            else if (operation.IsShowingQuestion)
            {
                await Clients.Group(roomName).SendAsync("question", Question[roomName]);
            }
            else if (operation.IsShowingResult)
            {

            }
            else if (operation.IsShowingAnswer)
            {

            }

            await Clients.Group(roomName).SendAsync("operationFlag", operation);
        }

        public void SetTime(string roomName)
        {
            Time[roomName] += 1;
            Console.WriteLine("{0:D2}:{1:D2}", Time[roomName] / 60, Time[roomName] % 60);
        }

        public Task GetTime(string roomName)
        {
            var time = Time[roomName];
            var timeFormat = string.Format("{0:D2}:{1:D2}", time/60, time%60);

            return Clients.Group(roomName).SendAsync("time", timeFormat);
        }

        private Task EmitJoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        private Task EmitCreated(string roomName)
        {
            return Clients.Caller.SendAsync("created", ConnectedClients[roomName].ToList());
        }

        private Task EmitLeft(string roomName)
        {
            return Clients.Group(roomName).SendAsync("left", ConnectedClients[roomName].ToList());
        }

        private Task EmitJoined(string roomName)
        {
            return Clients.Group(roomName).SendAsync("joined", ConnectedClients[roomName].ToList());
        }

        private Task EmitLog(string message, string roomName)
        {
            return Clients.Group(roomName).SendAsync("log", "[Server]: " + message);
        }
    }
}
