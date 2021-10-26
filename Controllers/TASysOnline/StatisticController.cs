using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            this._statisticService = statisticService;
        }


        [HttpGet]
        [Route("course-statistic")]
        [Authorize(Roles.Admin)]
        public async Task<IActionResult> CourseStatistic()
        {
            var response = await this._statisticService.GetCourseStatistic();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("instructor-statistic")]
        [Authorize(Roles.Admin)]
        public async Task<IActionResult> InstructorStatistic()
        {
            var response = await this._statisticService.GetInstructorStatistic();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("learner-statistic")]
        [Authorize(Roles.Admin)]
        public async Task<IActionResult> LearnerStatistic()
        {
            var response = await this._statisticService.GetLearnerStatisticResponse();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("streamsession-statistic")]
        [Authorize(Roles.Admin)]
        public async Task<IActionResult> StreamSessionStatistic()
        {
            var response = await this._statisticService.GetStreamSessionStatistic();
            return StatusCode(response.StatusCode, response);
        }
    }
}
