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
    public class UserInfoController : ControllerBase
    {
        private IUserInfoService _userInfoService;

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

        public UserInfoController(IUserInfoService UserInfoService)
        {
            this._userInfoService = UserInfoService;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserInfoByUserAccountId(Guid userId)
        {
            var response = await this._userInfoService.GetUserInfoById(userId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserInfoRequest userInfoRequest)
        {

            var response = await this._userInfoService.UpdateUserInfo(userInfoRequest, this.GetAccountAuthorInfo());

            return StatusCode(response.StatusCode, response);
        }

    }
}
