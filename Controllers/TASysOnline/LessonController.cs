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
    public class LessonController : ControllerBase
    {
        private ILessonService _LessonService;

        public LessonController(ILessonService LessonService)
        {
            this._LessonService = LessonService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLesson()
        {
            var responses = await this._LessonService.GetAllLessonAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        public async Task<IActionResult> GetAllLessonPaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._LessonService.GetAllLessonPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetLessonById(Guid id)
        {
            var response = await this._LessonService.GetLessonById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchLesson([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._LessonService.SearchLessonBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> FilterLesson([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._LessonService.FilterLessonBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> CreateLesson([FromBody] LessonRequest LessonRequest)
        {
            var response = await this._LessonService.CreateLessonAsync(LessonRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> UpdateLesson([FromBody] LessonRequest LessonRequest)
        {
            var response = await this._LessonService.UpdateLesson(LessonRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> DeleteLesson([FromBody] Guid[] LessonId)
        {
            var response = await this._LessonService.DeleteLesson(LessonId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllLesson()
        {
            var response = await this._LessonService.DeleteAllLesson();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("filter-search")]
        public async Task<IActionResult> FilterSearch([FromQuery] FilterSearch filterSearchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._LessonService.FilterSearchLessonBy(filterSearchRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }
    }
}
