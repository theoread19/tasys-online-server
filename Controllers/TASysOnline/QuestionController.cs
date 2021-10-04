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
    public class QuestionController : ControllerBase
    {
        private IQuestionService _QuestionService;

        public QuestionController(IQuestionService QuestionService)
        {
            this._QuestionService = QuestionService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllQuestion()
        {
            var responses = await this._QuestionService.GetAllQuestionAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetAllQuestionPaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._QuestionService.GetAllQuestionPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetQuestionById(Guid id)
        {
            var response = await this._QuestionService.GetQuestionById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> SearchSubject([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._QuestionService.SearchQuestionBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> FilterQuestion([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._QuestionService.FilterQuestionBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.All)]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionRequest QuestionRequest)
        {
            var response = await this._QuestionService.CreateQuestionAsync(QuestionRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.All)]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> UpdateQuestion([FromBody] QuestionRequest QuestionRequest)
        {
            var response = await this._QuestionService.UpdateQuestion(QuestionRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.All)]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> DeleteQuestion([FromBody] Guid[] QuestionId)
        {
            var response = await this._QuestionService.DeleteQuestion(QuestionId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> DeleteAllQuestion()
        {
            var response = await this._QuestionService.DeleteAllQuestion();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("filter-search")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> FilterSearchQuestionBy([FromQuery] FilterSearch filterSearch)
        {
            var route = Request.Path.Value;
            var responses = await this._QuestionService.FilterSearchQuestionBy(filterSearch, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }
    }
}
