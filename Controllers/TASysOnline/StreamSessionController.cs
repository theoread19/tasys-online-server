using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamSessionController : ControllerBase
    {
        private IStreamSessionService _StreamSessionService;

        private ICourseService _courseService;

        public StreamSessionController(IStreamSessionService StreamSessionService, ICourseService courseService)
        {
            this._StreamSessionService = StreamSessionService;
            this._courseService = courseService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllStreamSession()
        {
            var responses = await this._StreamSessionService.GetAllStreamSessionAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetAllStreamSessionPaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._StreamSessionService.GetAllStreamSessionPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetStreamSessionById(Guid id)
        {
            var response = await this._StreamSessionService.GetStreamSessionById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> SearchStreamSession([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._StreamSessionService.SearchStreamSessionBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> FilterStreamSession([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._StreamSessionService.FilterStreamSessionBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> CreateStreamSession([FromBody] StreamSessionRequest streamSessionRequest)
        {
            var response = await this._StreamSessionService.CreateStreamSessionAsync(streamSessionRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> UpdateStreamSession([FromBody] StreamSessionRequest streamSessionRequest)
        {
            var response = await this._StreamSessionService.UpdateStreamSession(streamSessionRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> DeleteStreamSession([FromBody] Guid[] streamSessionId)
        {
            var response = await this._StreamSessionService.DeleteStreamSession(streamSessionId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllStreamSession()
        {
            var response = await this._StreamSessionService.DeleteAllStreamSession();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("{courseId}/export/roll-call")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> ExportFile(Guid courseId, [FromBody] List<UserAccountAuthRequest> learner)
        {
            var course = await this._courseService.GetCourseById(courseId);

            if (course.StatusCode == StatusCodes.Status404NotFound)
            {
                return StatusCode(course.StatusCode, course);
            }

            var stream = await this._StreamSessionService.ExportDBToExcel(learner);
            var now = DateTime.Today;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", course.Name + "-" + now + ".xlsx");
        }
    }
}
