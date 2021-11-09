using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Service.TASysOnline;
using TASysOnlineProject.Service.TASysOnline.impl;

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostLikeController : ControllerBase
    {
        private IPostLikeService _postLikeService;

        public PostLikeController(IPostLikeService postLikeService)
        {
            this._postLikeService = postLikeService;
        }

        [HttpPost]
        public async Task<IActionResult> LikePost([FromBody] PostLikeRequest postLikeRequest)
        {
            var response = await this._postLikeService.LikeOrUnlikePost(postLikeRequest);
            return StatusCode(response.StatusCode, response);
        }
    }
}
