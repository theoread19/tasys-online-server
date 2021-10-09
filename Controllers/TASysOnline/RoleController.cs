using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Service.TASysOnline;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TASysOnlineProject.Controllers.TASysOnline
{

    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public IRoleService _roleService;

        /// <summary>
        ///     Initialize
        /// </summary>
        /// <param name="roleService"></param>
        /// <param name="loggerManager"></param>
        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        /// <summary>
        ///     Controler get all roles
        /// </summary>
        /// <returns> asynchronous return IActionResult list of role</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            var responses = await this._roleService.GetAllRoleAsync();
            return StatusCode(StatusCodes.Status200OK, responses);
        }
        

        // POST api/<RoleController>
        /// <summary>
        ///     Create role
        /// </summary>
        /// <param name="roleRequest"></param>
        /// <returns> asynchronous return IActionResult</returns>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequest roleRequest)
        {
            var response = await this._roleService.CreateAsync(roleRequest);
            return StatusCode(response.StatusCode, response);
        }
    }
}
