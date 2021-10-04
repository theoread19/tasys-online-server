using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class StatisticService : IStatisticService
    {

        private readonly IUserAccountService _userAccountService;

        private readonly ICourseService _courseService;

        public StatisticService(IUserAccountService userAccountService, ICourseService courseService)
        {
            this._courseService = courseService;
            this._userAccountService = userAccountService;
        }

        public async Task<StatisticResponse> GetStatistics()
        {
            var countLearner = await this._userAccountService.CountByRoleIdAsync(new Guid(Roles.LearnerId));
            var countInstructor = await this._userAccountService.CountByRoleIdAsync(new Guid(Roles.InstructorId));
            var countCourse = await this._courseService.CountAsync();

            var response = new StatisticResponse { CountCourse = countCourse, 
                                                CountInstructor = countInstructor, 
                                                CountLeaner = countLearner };

            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Fetching all datas for statistic susscessfully!";
            return response;
        }
    }
}
