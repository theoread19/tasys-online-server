using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Data.Responses.StatisticResponses;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class StatisticService : IStatisticService
    {

        private readonly IUserAccountService _userAccountService;

        private readonly ICourseService _courseService;

        private readonly IStreamSessionService _streamSessionService;

        public StatisticService(IUserAccountService userAccountService, ICourseService courseService, IStreamSessionService streamSessionService)
        {
            this._courseService = courseService;
            this._userAccountService = userAccountService;
            this._streamSessionService = streamSessionService;
        }

        public async Task<CourseStatisticResponse> GetCourseStatistic()
        {
            var countCourse = await this._courseService.CountAsync();

            return new CourseStatisticResponse
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Fectching data statistic successfully!",
                Count = countCourse
            };
        }

        public async Task<InstructorStatisticResponse> GetInstructorStatistic()
        {
            var countInstructor = await this._userAccountService.CountByRoleIdAsync(new Guid(Roles.InstructorId));

            return new InstructorStatisticResponse
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Fectching data statistic successfully!",
                Count = countInstructor
            };
        }

        public async Task<LearnerStatisticResponse> GetLearnerStatisticResponse()
        {
            var countLearner = await this._userAccountService.CountByRoleIdAsync(new Guid(Roles.LearnerId));

            return new LearnerStatisticResponse
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Fectching data statistic successfully!",
                Count = countLearner
            };
        }

        public async Task<StreamSessionStatisticResponse> GetStreamSessionStatistic()
        {
            var countStreamSession = await this._streamSessionService.CountAsync();

            return new StreamSessionStatisticResponse
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Fectching data statistic successfully!",
                Count = countStreamSession
            };
        }
    }
}
