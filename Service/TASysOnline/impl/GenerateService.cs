using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Enum;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Table;
using TASysOnlineProject.Table.Identity;
using BC = BCrypt.Net.BCrypt;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class GenerateService : IGenerateService
    {
        private readonly IRoleRepository _roleRepository;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IMapper _mapper;

        private readonly IUserAccountRepository _userAccountRepository;

        private readonly ICourseService _courseService;

        private readonly ISubjectRepository _subjectRepository;

        private readonly ITestService _testService;

        private readonly IQuestionService _questionService;

        private readonly ILessonService _lessonService;

        private readonly IStreamSessionService _streamSessionService;

        private readonly IAuthorService _authorService;

        private Random _random = new Random();

        public GenerateService(IRoleRepository roleRepository,
                                RoleManager<IdentityRole> roleManager,
                                IMapper mapper,
                                IUserAccountRepository userAccountRepository,
                                ICourseService courseService,
                                ISubjectRepository subjectRepository,
                                ITestService testService,
                                IQuestionService questionService,
                                ILessonService lessonService,
                                IStreamSessionService streamSessionService,
                                IAuthorService authorService)
        {
            this._roleRepository = roleRepository;
            this._roleManager = roleManager;
            this._mapper = mapper;
            this._userAccountRepository = userAccountRepository;
            this._courseService = courseService;
            this._subjectRepository = subjectRepository;
            this._testService = testService;
            this._questionService = questionService;
            this._lessonService = lessonService;
            this._streamSessionService = streamSessionService;
            this._authorService = authorService;
        }

        public async Task GenerateCourseData()
        {
            var instructor = await this._userAccountRepository.FindByUsernameAsync("instructor");
            var subject = await this._subjectRepository.GetAllAsync();
            var subjectId = subject.FirstOrDefault().Id;
            CourseRequest data = new CourseRequest
            {
                AvailableSlot = 5,
                Cost = 8,
                Description = "Generate",
                Duration = 45,
                Id = Guid.NewGuid(),
                MaxSlot = 40,
                Name = "Generate" + Guid.NewGuid(),
                Summary = "Summary",
                InstructorId = instructor.Id,
                SubjectId = subjectId
            };

            await this._courseService.CreateCourseAsync(data);
        }

        public async Task GenerateLessonData()
        {
            var course = await this._courseService.FindByNameAsync("Generate");

            var datas = new List<LessonRequest>
            {
                new LessonRequest { BackText = "A device for storing information on a computer", Description = "Generate", FrontText = $"disk", Name = "Generate", CourseId = course.Id },
                new LessonRequest { BackText = "To make an action possible or easier", Description = "Generate", FrontText = $"facilitate", Name = "Generate", CourseId = course.Id },
                new LessonRequest { BackText = "A number of computers and other devices that are connected together", Description = "Generate", FrontText = $"network", Name = "Generate", CourseId = course.Id },
                new LessonRequest { BackText = "The state of being liked by a large number of people", Description = "Generate", FrontText = $"popularity", Name = "Generate", CourseId = course.Id },
                new LessonRequest { BackText = "A series of something that are done in order to achieve a particular result", Description = "Generate", FrontText = $"process", Name = "Generate", CourseId = course.Id },
                new LessonRequest { BackText = "To be used instead of something / somebody else", Description = "Generate", FrontText = $"replace", Name = "Generate", CourseId = course.Id },
                new LessonRequest { BackText = "A great change in conditions, ways of working, beliefs, etc. ..that affects large numbers of people", Description = "Generate", FrontText = $"revolution", Name = "Generate", CourseId = course.Id },
                new LessonRequest { BackText = "Sudden and rapid, especially of a change in something", Description = "Generate", FrontText = $"sharp", Name = "Generate", CourseId = course.Id },
                new LessonRequest { BackText = "The ability to do something well", Description = "Generate", FrontText = "skill", Name = "Generate", CourseId = course.Id },
                new LessonRequest { BackText = "The programs, etc....used to operate a computer", Description = "Generate", FrontText = $"software", Name = "Generate", CourseId = course.Id },
            };

            var randomNumer = this._random.Next(0, 9);
            var data = datas[randomNumer];

            await this._lessonService.CreateLessonAsync(data);
        }

        public async Task GenerateQuestionsData()
        {
            var tests = await this._testService.GetAllTestAsync();
            var testId = tests.FirstOrDefault().Id;

            var datas = new List<QuestionRequest>
            {
                new QuestionRequest
                {
                    Content = "1 + 1 = ?",
                    Score = 1,
                    TotalCorrectAnswer = 1,
                    TestId = testId,
                    AnswerRequests = new List<AnswerRequest>
                    {
                        new AnswerRequest { Content = "10", IsCorrect = false},
                        new AnswerRequest { Content = "2", IsCorrect = true}
                    }
                },
                new QuestionRequest
                {
                    Content = "How many legs does a spider have?",
                    Score = 1,
                    TotalCorrectAnswer = 1,
                    TestId = testId,
                    AnswerRequests = new List<AnswerRequest>
                    {
                        new AnswerRequest { Content = "10", IsCorrect = false},
                        new AnswerRequest { Content = "8", IsCorrect = true}
                    }
                },
                new QuestionRequest
                {
                    Content = "How many planets are in our solar system?",
                    Score = 1,
                    TotalCorrectAnswer = 1,
                    TestId = testId,
                    AnswerRequests = new List<AnswerRequest>
                    {
                        new AnswerRequest { Content = "12", IsCorrect = false},
                        new AnswerRequest { Content = "8", IsCorrect = true}
                    }
                },
                new QuestionRequest
                {
                    Content = "How many days are in a year?",
                    Score = 1,
                    TotalCorrectAnswer = 1,
                    TestId = testId,
                    AnswerRequests = new List<AnswerRequest>
                    {
                        new AnswerRequest { Content = "367", IsCorrect = false},
                        new AnswerRequest { Content = "365", IsCorrect = true}
                    }
                },
                new QuestionRequest
                {
                    Content = "In sports, what is an MVP?",
                    Score = 1,
                    TotalCorrectAnswer = 1,
                    TestId = testId,
                    AnswerRequests = new List<AnswerRequest>
                    {
                        new AnswerRequest { Content = "Minimum Viable Product", IsCorrect = false},
                        new AnswerRequest { Content = "Most Valuable Player", IsCorrect = true}
                    }
                },
                new QuestionRequest
                {
                    Content = "What is the hardest natural substance?",
                    Score = 1,
                    TotalCorrectAnswer = 1,
                    TestId = testId,
                    AnswerRequests = new List<AnswerRequest>
                    {
                        new AnswerRequest { Content = "Iron", IsCorrect = false},
                        new AnswerRequest { Content = "Diamond", IsCorrect = true}
                    }
                },
                new QuestionRequest
                {
                    Content = "Where does the President of the United States live while in office?",
                    Score = 1,
                    TotalCorrectAnswer = 1,
                    TestId = testId,
                    AnswerRequests = new List<AnswerRequest>
                    {
                        new AnswerRequest { Content = "The Black House", IsCorrect = false},
                        new AnswerRequest { Content = "The White House", IsCorrect = true}
                    }
                },
                new QuestionRequest
                {
                    Content = "What is the opposite of 'cheap'?",
                    Score = 1,
                    TotalCorrectAnswer = 1,
                    TestId = testId,
                    AnswerRequests = new List<AnswerRequest>
                    {
                        new AnswerRequest { Content = "Explosion", IsCorrect = false},
                        new AnswerRequest { Content = "Expensive", IsCorrect = true}
                    }
                },
                new QuestionRequest
                {
                    Content = "What food do pandas eat?",
                    Score = 1,
                    TotalCorrectAnswer = 1,
                    TestId = testId,
                    AnswerRequests = new List<AnswerRequest>
                    {
                        new AnswerRequest { Content = "Apple", IsCorrect = false},
                        new AnswerRequest { Content = "Bamboo", IsCorrect = true}
                    }
                },
                 new QuestionRequest
                {
                    Content = "Can you name the closest star to Earth?",
                    Score = 1,
                    TotalCorrectAnswer = 1,
                    TestId = testId,
                    AnswerRequests = new List<AnswerRequest>
                    {
                        new AnswerRequest { Content = "The moon", IsCorrect = false},
                        new AnswerRequest { Content = "The sun", IsCorrect = true}
                    }
                },
            };
            var randomNumer = this._random.Next(0, 9);
            var data = datas[randomNumer];
            await this._questionService.CreateQuestionAsync(data);
        }

        public async Task GenerateRoleData()
        {
            var datas = new List<RoleTable> {
                new RoleTable{ Id = new Guid(Roles.AdminId), Name = Roles.Admin},
                new RoleTable{ Id = new Guid(Roles.InstructorId), Name = Roles.Instructor},
                new RoleTable{ Id = new Guid(Roles.LearnerId), Name = Roles.Learner}
            };

            foreach (var data in datas)
            {
                await this._roleRepository.InsertAsync(data);
                await this._roleManager.CreateAsync(new IdentityRole
                {
                    Name = data.Name
                });
            }

            await this._roleRepository.SaveAsync();
        }

        public async Task GenerateStreamData()
        {
            var course = await this._courseService.FindByNameAsync("Generate");

            var instructor = await this._userAccountRepository.FindByUsernameAsync("instructor");

            var randomDate = this._random.Next(0, 31);

            var data = new StreamSessionRequest
            {
                StartTime = DateTime.UtcNow.AddDays(randomDate),
                EndTime = DateTime.UtcNow.AddDays(randomDate),
                CourseId = course.Id,
                CreatorId = instructor.Id,
            };

            await this._streamSessionService.CreateStreamSessionAsync(data);
        }

        public async Task GenerateSubjectData()
        {
            var datas = new List<SubjectTable> {
                new SubjectTable { Id = Guid.NewGuid(), Name = "Math"},
                new SubjectTable { Id = Guid.NewGuid(), Name = "English"},
                new SubjectTable { Id = Guid.NewGuid(), Name = "Progaming"},
                new SubjectTable { Id = Guid.NewGuid(), Name = "Physics"},
                new SubjectTable { Id = Guid.NewGuid(), Name = "Chemistry"},
            };
            foreach (var data in datas)
            {
                await this._subjectRepository.InsertAsync(data);
            }

            await this._subjectRepository.SaveAsync();
        }

        public async Task GenerateTestData()
        {
            var course = await this._courseService.FindByNameAsync("Generate");

            var data = new TestRequest
            {
                AllocatedTime = 120,
                Deadline = DateTime.UtcNow.AddDays(30),
                Description = "Generate",
                MaxAttempt = 1000,
                MaxScore = 1000,
                TotalQuestions = 1000,
                CourseId = course.Id,
                Name = "Generate"
            };

            await this._testService.CreateTestAsync(data);
        }

        public async Task GenerateUserAccountData()
        {
            var datas = new List<RegisterRequest> {
                new RegisterRequest { DisplayName = "ADMIN", Password = "admin", Username = "admin", RoleId = new Guid(Roles.AdminId)},
                new RegisterRequest { DisplayName = "INSTRUCTOR", Password = "instructor", Username = "instructor", RoleId = new Guid(Roles.InstructorId)},
                new RegisterRequest { DisplayName = "LEARNER", Password = "leaner", Username = "leaner", RoleId = new Guid(Roles.LearnerId)}
            };

            foreach (var data in datas)
            {
                await this._authorService.RegisterAsync(data);
            }
        }
    }
}
