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
    public class PostController : ControllerBase
    {
        private IPostService _postService;
        
        public PostController(IPostService postService)
        {
            this._postService = postService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllPost()
        {
            var responses = await this._postService.GetAllPostAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetAllPostPaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._postService.GetAllPostPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var response = await this._postService.GetPostById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> SearchSubject([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._postService.SearchPostBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> FilterPost([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._postService.FilterPostBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest postRequest)
        {
            //them courseId
            var response = await this._postService.CreatePostAsync(postRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> UpdatePost([FromBody] PostRequest postRequest)
        {
            var response = await this._postService.UpdatePost(postRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> DeletePost([FromBody] Guid[] PostId)
        {
            var response = await this._postService.DeletePost(PostId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllPost()
        {
            var response = await this._postService.DeleteAllPost();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("filter-search")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> FilterSearchPost([FromQuery] FilterSearch filterSearchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._postService.FilterSearchPostBy(filterSearchRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }
    }
}
