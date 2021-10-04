using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Service.TASysOnline;
using TASysOnlineProject.Table;
using BC = BCrypt.Net.BCrypt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        private readonly IAuthorService _authorService;


        /// <summary>
        ///     Initialize
        /// </summary>
        /// <param name="userAccountService"></param>
        /// <param name="loggerManager"></param>
        /// <param name="roleService"></param>
        /// <param name="configuration"></param>
        public AuthenticateController(IAuthorService authorService)
        {
            this._authorService = authorService;
        }


        /// <summary>
        ///     Controller for login
        /// </summary>
        /// <param name="loginRequest">
        ///     User account
        /// </param>
        /// <returns>
        ///     asynchronous return token for access api
        /// </returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await this._authorService.AuthorAsync(loginRequest);

            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        ///     Controller for register a account
        /// </summary>
        /// <param name="registerRequest">
        ///     a user account
        /// </param>
        /// <returns> asynchronous return IActionResult</returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var response = await this._authorService.RegisterAsync(registerRequest);

            return StatusCode(response.StatusCode, response);
        }
    }
}
