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
    public class BillController : ControllerBase
    {
        private IBillService _BillService;

        public BillController(IBillService BillService)
        {
            this._BillService = BillService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllBill()
        {
            var responses = await this._BillService.GetAllBillAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        public async Task<IActionResult> GetAllBillPaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._BillService.GetAllBillPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBillById(Guid id)
        {
            var response = await this._BillService.GetBillById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchSubject([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._BillService.SearchBillBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> FilterBill([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._BillService.FilterBillBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> CreateBill([FromBody] BillRequest BillRequest)
        {
            var response = await this._BillService.CreateBillAsync(BillRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteBill([FromBody] Guid[] BillId)
        {
            var response = await this._BillService.DeleteBill(BillId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllBill()
        {
            var response = await this._BillService.DeleteAllBill();
            return StatusCode(response.StatusCode, response);
        }
    }
}
