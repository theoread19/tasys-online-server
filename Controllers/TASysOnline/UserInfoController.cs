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
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService UserInfoService)
        {
            this._userInfoService = UserInfoService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllUserInfo()
        {
            var responses = await this._userInfoService.GetAllUserInfoAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserInfoByUserAccountId(Guid id)
        {
            var response = await this._userInfoService.GetUserInfoById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserInfoRequest userInfoRequest)
        {
            var user = HttpContext.User;

            var role = user.FindFirst(ClaimTypes.Role).Value;

            if(role != Roles.Admin)
            {
                var userId = new Guid(user.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (userId != userInfoRequest.UserAccountId)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Invalid access data!");
                }
            }

            var response = await this._userInfoService.UpdateUserInfo(userInfoRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteAllUserInfo()
        {
            var response = await this._userInfoService.DeleteAllUserInfo();
            return StatusCode(response.StatusCode, response);
        }
    }
}
