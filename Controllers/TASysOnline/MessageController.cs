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
    public class MessageController : ControllerBase
    {
        private IMessageService _MessageService;

        public MessageController(IMessageService messageService)
        {
            this._MessageService = messageService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllMessage()
        {
            var responses = await this._MessageService.GetAllMessageAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetAllMessagePaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._MessageService.GetAllMessagePagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetMessageById(Guid id)
        {
            var response = await this._MessageService.GetMessageById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> SearchSubject([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._MessageService.SearchMessageBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> FilterMessage([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._MessageService.FilterMessageBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> CreateMessage([FromBody] MessageRequest messageRequest)
        {
            var response = await this._MessageService.CreateMessageAsync(messageRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> UpdateMessage([FromBody] MessageRequest messageRequest)
        {
            var response = await this._MessageService.UpdateMessage(messageRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> DeleteMessage([FromBody] Guid[] messageId)
        {
            var response = await this._MessageService.DeleteMessage(messageId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllMessage()
        {
            var response = await this._MessageService.DeleteAllMessage();
            return StatusCode(response.StatusCode, response);
        }
    }
}
