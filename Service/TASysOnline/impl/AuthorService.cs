using Microsoft.AspNetCore.Http;
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
using BC = BCrypt.Net.BCrypt;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class AuthorService : IAuthorService
    {
        private readonly IRoleService _roleService;

        private readonly IUserAccountService _userAccountService;

        private readonly IConfiguration _configuration;

        public AuthorService(IRoleService roleService, IUserAccountService userAccountService, IConfiguration configuration)
        {
            this._roleService = roleService;

            this._configuration = configuration;

            this._userAccountService = userAccountService;
        }

        public async Task<LoginResponse> AuthorAsync(LoginRequest loginRequest)
        {
            var user = await this._userAccountService.FindByUsernameForAuthorAsync(loginRequest.Username!);
            
            if (user == null)
            {
                return new LoginResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ResponseMessage = "Wrong username!"
                };
            }

            if(user.Status < 0)
            {
                return new LoginResponse
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    ResponseMessage = "User has been blocked!"
                };
            }

            if (!(BC.Verify(loginRequest.Password, user.Password)))
            {
                return new LoginResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ResponseMessage = "Wrong password!"
                };
            }


            var role = await this._roleService.FindByIdAsync(user.RoleId);
            
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role.Name),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(

                issuer: _configuration["JWT:ValidIssuer"],
                //expires: DateTime.Now.AddMinutes(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new LoginResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                RoleId = user.RoleId,
                Username = user.Username,
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Login successfully!"
            };
        }

        public async Task<Response> RegisterAsync(RegisterRequest registerRequest)
        {
            var response = await this._userAccountService.CreateUserAsync(new UserAccountRequest {Password = registerRequest.Password, DisplayName = registerRequest.DisplayName,Username = registerRequest.Username, RoleId = registerRequest.RoleId });

            return response;
        }
    }
}
