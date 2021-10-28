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

        private readonly ICurriCulumService _curriCulumService;

        private readonly ILessonService _lessonService;

        private readonly IStreamSessionService _streamSessionService;

        private readonly IAuthorService _authorService;

        public GenerateService(IRoleRepository roleRepository,
                                RoleManager<IdentityRole> roleManager,
                                IMapper mapper,
                                IUserAccountRepository userAccountRepository,
                                ICourseService courseService,
                                ISubjectRepository subjectRepository,
                                ITestService testService,
                                IQuestionService questionService,
                                ICurriCulumService curriCulumService,
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
            this._curriCulumService = curriCulumService;
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
                RatingCount = 0,
                MaxSlot = 40,
                Name = "Generate",
                Summary = "Summary",
                InstructorId = instructor.Id,
                Rating = 0,
                Feedback = "string",
                SubjectId = subjectId
            };

            await this._courseService.CreateCourseAsync(data);
        }

        public async Task GenerateCurriCulumData()
        {
            var course = await this._courseService.FindByNameAsync("Generate");
            var datas = new List<CurriCulumRequest>
            {
                new CurriCulumRequest { CourseId = course.Id, Name = "Generate 1"},
                new CurriCulumRequest { CourseId = course.Id, Name = "Generate 2"},
                new CurriCulumRequest { CourseId = course.Id, Name = "Generate 3"},
                new CurriCulumRequest { CourseId = course.Id, Name = "Generate 4"},
            };

            foreach (var data in datas)
            {
                await this._curriCulumService.CreateCurriCulumAsync(data);
            }
        }

        public async Task GenerateLessonData()
        {
            var course = await this._courseService.FindByNameAsync("Generate");

            var data = new LessonRequest { BackText = "back", Description = "Generate", FrontText = "front", Name = "Generate", CourseId = course.Id };

            await this._lessonService.CreateLessonAsync(data);
        }

        public async Task GenerateQuestionsData()
        {
            var tests = await this._testService.GetAllTestAsync();
            var testId = tests.FirstOrDefault().Id;

            var data = new QuestionRequest
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
            };

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

            var data = new StreamSessionRequest
            {
                StartTime = DateTime.UtcNow.AddDays(31).ToString(),
                EndTime = DateTime.UtcNow.AddDays(31).AddMinutes(50).ToString(),
                CourseId = course.Id,
                CreatorId = instructor.Id,
                MaxParticipants = await this._courseService.CountLeanerOfCourse(course.Id)
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
                MaxScore = 1,
                TotalAttempt = 0,
                TotalQuestions = 1,
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
