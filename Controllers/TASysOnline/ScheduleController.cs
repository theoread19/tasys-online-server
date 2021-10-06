using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [Route("generate-data")]
        public async Task<IActionResult> Generate()
        {
            await this._scheduleService.GenerateData();
            return Ok();
        }
    }
}
