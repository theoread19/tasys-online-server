using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultController : ControllerBase
    {
        private ITestResultService _TestResultService;

        public TestResultController(ITestResultService testResultService)
        {
            this._TestResultService = testResultService;
        }

        [HttpGet]
        [Route("filter")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> FilterTestResult([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._TestResultService.FilterTestResultBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> CalculateTestResult([FromBody] DoTestRequest doTestRequest)
        {

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != doTestRequest.UserId.ToString())
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Invalid access data!");
            }

            var response = await this._TestResultService.CalculateTestResult(doTestRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("{userId}/test/{testId}")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetTestResultByUserIdAndTestId(Guid userId, Guid testId)
        {
            var responses = await this._TestResultService.GetTestResultByTestIdAndUserIdAsync(userId, testId);

            return StatusCode(StatusCodes.Status200OK, responses);
        }
    }
}
