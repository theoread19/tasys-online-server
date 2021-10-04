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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            this._discountService = discountService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllDiscount()
        {
            var responses = await this._discountService.GetAllDiscountAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        public async Task<IActionResult> GetAllDiscountPaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._discountService.GetAllDiscountPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{courseId}")]
        public async Task<IActionResult> GetDiscountById(Guid courseId)
        {
            var response = await this._discountService.FindDiscountByCourseId(courseId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchSubject([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._discountService.SearchDiscountBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> FilterDiscount([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._discountService.FilterDiscountBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> CreateDiscount([FromBody] DiscountRequest discountRequest)
        {
            var response = await this._discountService.CreateDiscountAsync(discountRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> UpdateDiscount([FromBody] DiscountRequest discountRequest)
        {
            var response = await this._discountService.UpdateDiscount(discountRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> DeleteDiscount([FromBody] Guid[] DiscountId)
        {
            var response = await this._discountService.DeleteDiscount(DiscountId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> DeleteAllDiscount()
        {
            var response = await this._discountService.DeleteAllDiscount();
            return StatusCode(response.StatusCode, response);
        }
    }
}
