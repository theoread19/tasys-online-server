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
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllTestResult()
        {
            var responses = await this._TestResultService.GetAllTestResultAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        public async Task<IActionResult> GetAllTestResultPaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._TestResultService.GetAllTestResultPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetTestResultById(Guid id)
        {
            var response = await this._TestResultService.GetTestResultById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchTestResult([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._TestResultService.SearchTestResultBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
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
        [Route("delete")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> DeleteTestResult([FromBody] Guid[] TestResultId)
        {
            var response = await this._TestResultService.DeleteTestResult(TestResultId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllTestResult()
        {
            var response = await this._TestResultService.DeleteAllTestResult();
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
