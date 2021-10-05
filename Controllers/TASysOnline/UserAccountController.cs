﻿using Microsoft.AspNetCore.Authorization;
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
    public class UserAccountController : ControllerBase
    {
        public IUserAccountService _userAccountService;

        public UserAccountController(IUserAccountService subjectService)
        {
            this._userAccountService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubject()
        {
            var responses = await this._userAccountService.GetAllUserAccountAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
        public async Task<IActionResult> GetAllCoursePaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._userAccountService.GetAllUserAccountPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchSubject([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._userAccountService.SearchUserAccountBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserAccountById(Guid id)
        {
            var response = await this._userAccountService.FindByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> FilterCourse([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._userAccountService.FilterUserAccountBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.All)]
        [Route("{userId}/password")]
        public async Task<IActionResult> ChangeUserAccountPassword(Guid userId, [FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var response = await this._userAccountService.ChangeUserAccountPasswordAsync(userId, changePasswordRequest);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAccount([FromBody] UserAccountRequest userAccountRequest)
        {
            var response = await this._userAccountService.CreateUserAsync(userAccountRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> UpdateUserAccount([FromBody] UserAccountRequest userAccountRequest)
        {
            var response = await this._userAccountService.UpdateUserAccount(userAccountRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteUserAccount([FromBody] Guid[] courseId)
        {
            var response = await this._userAccountService.DeleteUserAccount(courseId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteAllUserAccount()
        {
            var response = await this._userAccountService.DeleteAllUserAccount();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("filter-search")]
        public async Task<IActionResult> FilterSearchUserAccount([FromQuery] FilterSearch filterSearchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._userAccountService.FilterSearchUserAccountBy(filterSearchRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Route("block")]
        public  async Task<IActionResult> BlockUserAccount([FromBody] Guid userId)
        {
            var response = await this._userAccountService.BlockUserAccount(userId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("Generate-data")]
        public async Task<IActionResult> Generate()
        {
            await this._userAccountService.GenerateData();
            return Ok();
        }
    }
}
