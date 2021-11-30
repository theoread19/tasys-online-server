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
    public class CartController : ControllerBase
    {
        private ICartService _CartService;

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

        public CartController(ICartService CartService)
        {
            this._CartService = CartService;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetCartByUserId(Guid userId)
        {
            var response = await this._CartService.GetCartByUserId(userId, this.GetAccountAuthorInfo());
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("{userId}/add-to-cart")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Learner)]
        public async Task<IActionResult> AddCourseToCart(Guid userId, [FromBody] CourseRequest courseRequest)
        {
            var userInfo = this.GetAccountAuthorInfo();

            if (userInfo.Id != userId || userInfo.Role != Roles.Admin)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Invalid access data!");
            }
            var response = await this._CartService.AddCourseToCart(userId, courseRequest.Id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("{userId}/remove-from-cart")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Learner)]
        public async Task<IActionResult> RemoveCourseFromCart(Guid userId, [FromBody] CourseRequest courseRequest)
        {
            var userInfo = this.GetAccountAuthorInfo();

            if(userInfo.Id != userId || userInfo.Role != Roles.Admin)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Invalid access data!");
            }

            var response = await this._CartService.RemoveCourseFromCart(userId, courseRequest.Id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("{userId}/remove-all-from-cart")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Learner)]
        public async Task<IActionResult> RemoveAllCourseFromCart(Guid userId)
        {
            var userInfo = this.GetAccountAuthorInfo();

            if (userInfo.Id != userId || userInfo.Role != Roles.Admin)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Invalid access data!");
            }
            var response = await this._CartService.RemoveAllCourseFromCart(userId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
