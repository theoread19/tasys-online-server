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
    public class CurriCulumController : ControllerBase
    {
        private ICurriCulumService _CurriCulumService;

        public CurriCulumController(ICurriCulumService CurriCulumService)
        {
            this._CurriCulumService = CurriCulumService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllCurriCulum()
        {
            var responses = await this._CurriCulumService.GetAllCurriCulumAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        public async Task<IActionResult> GetAllCurriCulumPaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._CurriCulumService.GetAllCurriCulumPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCurriCulumById(Guid id)
        {
            var response = await this._CurriCulumService.GetCurriCulumById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchSubject([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._CurriCulumService.SearchCurriCulumBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> FilterCurriCulum([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._CurriCulumService.FilterCurriCulumBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> CreateCurriCulum([FromBody] CurriCulumRequest CurriCulumRequest)
        {
            var response = await this._CurriCulumService.CreateCurriCulumAsync(CurriCulumRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> UpdateCurriCulum([FromBody] CurriCulumRequest CurriCulumRequest)
        {
            var response = await this._CurriCulumService.UpdateCurriCulum(CurriCulumRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> DeleteCurriCulum([FromBody] Guid[] CurriCulumId)
        {
            var response = await this._CurriCulumService.DeleteCurriCulum(CurriCulumId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllCurriCulum()
        {
            var response = await this._CurriCulumService.DeleteAllCurriCulum();
            return StatusCode(response.StatusCode, response);
        }
    }
}
