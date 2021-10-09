using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController( IScheduleService scheduleService)
        {
            this._scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchedule()
        {
            var responses = await this._scheduleService.GetSchedule();
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles.Admin)]
        public async Task<IActionResult> CreateCart([FromBody] ScheduleRequest scheduleRequest)
        {
            var response = await this._scheduleService.CreateScheduleAsync(scheduleRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> UpdateCart([FromBody] ScheduleRequest scheduleRequest)
        {
            var response = await this._scheduleService.UpdateScheduleAsync(scheduleRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> DeleteCart([FromBody] Guid[] scheduleIds)
        {
            var response = await this._scheduleService.DeleteScheduleAsync(scheduleIds);

            return StatusCode(response.StatusCode, response);
        }
    }
}
