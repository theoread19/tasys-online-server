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
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private ITestService _TestService;

        private AccountAuthorInfo GetAccountAuthorInfo()
        {
            var user = HttpContext.User;

            return new AccountAuthorInfo
            {
                Id = new Guid(user.FindFirst(ClaimTypes.NameIdentifier).Value),
                Role = user.FindFirst(ClaimTypes.Role).Value,
                Username = user.FindFirst(ClaimTypes.Name).Value
            };
        }

        public TestController(ITestService TestService)
        {
            this._TestService = TestService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllTest()
        {
            var responses = await this._TestService.GetAllTestAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetAllTestPaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._TestService.GetAllTestPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetTestById(Guid id)
        {
            var response = await this._TestService.GetTestById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> SearchTest([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._TestService.SearchTestBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> FilterTest([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._TestService.FilterTestBy(filterRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> CreateTest([FromBody] TestRequest TestRequest)
        {
            var response = await this._TestService.CreateTestAsync(TestRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> UpdateTest([FromBody] TestRequest TestRequest)
        {
            var response = await this._TestService.UpdateTest(TestRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> DeleteTest([FromBody] Guid[] TestId)
        {
            var response = await this._TestService.DeleteTest(TestId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllTest()
        {
            var response = await this._TestService.DeleteAllTest();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("filter-search")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> FilterSearchTestBy([FromQuery] FilterSearch filterSearch)
        {
            var route = Request.Path.Value;
            var responses = await this._TestService.FilterSearchTestBy(filterSearch, route);
            return StatusCode(responses.StatusCode, responses);
        }
    }
}
