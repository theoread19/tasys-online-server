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
    public class CartController : ControllerBase
    {
        private ICartService _CartService;

        public CartController(ICartService CartService)
        {
            this._CartService = CartService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllCart()
        {
            var responses = await this._CartService.GetAllCartAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        public async Task<IActionResult> GetAllCartPaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._CartService.GetAllCartPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCartById(Guid id)
        {
            var response = await this._CartService.GetCartById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchSubject([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._CartService.SearchCartBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> FilterCart([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._CartService.FilterCartBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> CreateCart([FromBody] CartRequest CartRequest)
        {
            var response = await this._CartService.CreateCartAsync(CartRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> UpdateCart([FromBody] CartRequest CartRequest)
        {
            var response = await this._CartService.UpdateCart(CartRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> DeleteCart([FromBody] Guid[] CartId)
        {
            var response = await this._CartService.DeleteCart(CartId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllCart()
        {
            var response = await this._CartService.DeleteAllCart();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("{userId}/add-to-cart")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Learner)]
        public async Task<IActionResult> AddCourseToCart([FromQuery] Guid userId, [FromBody] CourseRequest courseRequest)
        {
            var response = await this._CartService.AddCourseToCart(userId, courseRequest.Id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("{userId}/remove-from-cart")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Learner)]
        public async Task<IActionResult> RemoveCourseFromCart([FromQuery] Guid userId, [FromBody] CourseRequest courseRequest)
        {
            var response = await this._CartService.RemoveCourseFromCart(userId, courseRequest.Id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Route("{userId}/remove-all-from-cart")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Learner)]
        public async Task<IActionResult> RemoveAllCourseFromCart([FromQuery] Guid userId)
        {
            var response = await this._CartService.RemoveAllCourseFromCart(userId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
