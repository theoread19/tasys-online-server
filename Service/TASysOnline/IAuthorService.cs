using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IAuthorService
    {
        public Task<LoginResponse> AuthorAsync(LoginRequest loginRequest);

        public Task<Response> RegisterAsync(RegisterRequest registerRequest);
    }
}
