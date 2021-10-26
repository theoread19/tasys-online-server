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
    public class CommentController : ControllerBase
    {
        private ICommentService _CommentService;

        public CommentController(ICommentService commentService)
        {
            this._CommentService = commentService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllComment()
        {
            var responses = await this._CommentService.GetAllCommentAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetAllCommentPaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._CommentService.GetAllCommentPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            var response = await this._CommentService.GetCommentById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> SearchSubject([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._CommentService.SearchCommentBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> FilterComment([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._CommentService.FilterCommentBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> CreateComment([FromBody] CommentRequest commentRequest)
        {
            var response = await this._CommentService.CreateCommentAsync(commentRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> UpdateComment([FromBody] CommentRequest commentRequest)
        {
            var response = await this._CommentService.UpdateComment(commentRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> DeleteComment([FromBody] Guid[] commentId)
        {
            var response = await this._CommentService.DeleteComment(commentId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllComment()
        {
            var response = await this._CommentService.DeleteAllComment();
            return StatusCode(response.StatusCode, response);
        }
    }
}
