using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Config.HubConfig
{
    public class ClassroomHub : Hub
    {
        public static Dictionary<string, List<UserAccountAuthRequest>> ConnectedClients = new Dictionary<string, List<UserAccountAuthRequest>>();

        private readonly IStreamSessionService _streamSessionService;

        private readonly IPostLikeRepository _postLikeRepository;

        private readonly IPostService _postService;

        public ClassroomHub(IStreamSessionService streamSessionService, IPostLikeRepository postLikeRepository, IPostService postService)
        {
            this._streamSessionService = streamSessionService;
            this._postLikeRepository = postLikeRepository;
            this._postService = postService;
        }

        public Task CreateOrJoinClass(string className, UserAccountAuthRequest userAccountAuthRequest)
        {
            EmitLog("Received request to create or join room " + className + " from a client " + userAccountAuthRequest.DisplayName, className);

            if (!ConnectedClients.ContainsKey(className))
            {
                ConnectedClients.Add(className, new List<UserAccountAuthRequest>());
            }

            var user = ConnectedClients[className].Where(w => w.Id.Equals(userAccountAuthRequest.Id)).FirstOrDefault();

            if (!ConnectedClients[className].Contains(user))
            {
                ConnectedClients[className].Add(userAccountAuthRequest);
            }

            EmitJoinClass(className);

            var numberOfClients = ConnectedClients[className].Count;

            if (numberOfClients == 1)
            {
                EmitCreated(className);
                EmitLog("Client " + Context.ConnectionId + " created the room " + className, className);
            }
            else
            {
                EmitJoined(className);
                EmitLog("Client " + Context.ConnectionId + " joined the room " + className, className);
            }

            EmitLog("Room " + className + " now has " + numberOfClients + " client(s)", className);

            return Task.Run(() => { return; });
        }

        public Task LeaveClass(string className, UserAccountAuthRequest userAccountAuthRequest)
        {
            EmitLog("Received request to leave the room " + className + " from a client " + userAccountAuthRequest.DisplayName, className);

            var user = ConnectedClients[className].Where(w => w.Id.Equals(userAccountAuthRequest.Id)).FirstOrDefault();

            if (ConnectedClients.ContainsKey(className) && ConnectedClients[className].Contains(user))
            {
                ConnectedClients[className].Remove(user);
                EmitLeft(className);
                EmitLog("Client " + Context.ConnectionId + " left the room " + className, className);

                if (ConnectedClients[className].Count == 0)
                {
                    ConnectedClients.Remove(className);
                    EmitLog("Room " + className + " is now empty - resetting its state", className);
                }
            }

            return Groups.RemoveFromGroupAsync(Context.ConnectionId, className);
        }

        public async Task RemoveClass(string className)
        {
            ConnectedClients.Remove(className);
            await EmitLog("Room " + className + " is now empty - resetting its state", className);
        }

        public async Task SendMessage(string className, MessageRequest message)
        {
            var sender = ConnectedClients[className].Where(w => w.Id == message.SenderId).FirstOrDefault();
            //var recipient = ConnectedClients[className].Where(w => w.Id == message.RecipientId).FirstOrDefault();

            await Clients.Group(className).SendAsync("message", sender, message);
        }

        public async Task CreatePost(string className, PostRequest post)
        {
            await EmitLog("notify create post in room", className);

            await Clients.Group(className).SendAsync("post", post);

        }

        public async Task LikePost(string className, PostLikeRequest postLikeRequest)
        {
            await EmitLog("Like post", className);

            var postLike = await this._postLikeRepository.FindPostLikeByPostIdAndUserId(postLikeRequest.PostId, postLikeRequest.UserAccountId);

            await Clients.User(postLike.UserAccountId.ToString()).SendAsync("post", postLike);
        }

        public async Task CommentPost(string className, CommentRequest commentRequest)
        {
            await EmitLog("Comment post", className);

            var post = this._postService.GetPostById(commentRequest.PostId);

            await Clients.User(commentRequest.UserAccountId.ToString()).SendAsync("post", post, commentRequest);
        }

        public void Application_Start(object sender, EventArgs e)
        {
            var aTimer = new Timer(1000);
            aTimer.Elapsed += aTimer_Elapsed;

            aTimer.Interval = 1000;

            aTimer.Enabled = true;            

        }

        public void aTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //var context = GlobalHost.ConnectionManager.GetHubContext<ClassroomHub>();

            var now = DateTime.UtcNow;
            var streams = this._streamSessionService.GetComingStreamSessionAsync(now).Result;
            foreach (var stream in streams)
            {
                if(stream.StartTime.AddMinutes(5) == now || stream.StartTime.AddMinutes(1) == now || stream.StartTime == now)
                {
                    var courseName = stream.CourseTable.Name;
                    Clients.Group(courseName).SendAsync("notifyStream", stream);
                    
                }
            }
            //context.Clients.All.addMessage("This message broadcasted on " + DateTime.Now);
        }

        private Task EmitJoinClass(string className)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, className);
        }

        private Task EmitCreated(string className)
        {
            return Clients.Caller.SendAsync("created", ConnectedClients[className].ToList());
        }

        private Task EmitLeft(string className)
        {
            return Clients.Group(className).SendAsync("left", ConnectedClients[className].ToList());
        }

        private Task EmitJoined(string classMame)
        {
            return Clients.Group(classMame).SendAsync("joined", ConnectedClients[classMame].ToList());
        }

        private Task EmitLog(string message, string className)
        {
            return Clients.Group(className).SendAsync("log", "[Server]: " + message);
        }
    }
}
 