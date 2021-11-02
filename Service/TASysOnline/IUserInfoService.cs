using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IUserInfoService
    {
        public Task<IEnumerable<UserInfoResponse>> GetAllUserInfoAsync();

        public Task<Response> CreateUserInfoAsync(UserInfoRequest userInfoRequest);

        public Task<UserInfoResponse> GetUserInfoById(Guid id);

        public Task<Response> UpdateUserInfo(UserInfoRequest UserInfoRequest);

        public Task<Response> DeleteAllUserInfo();
    }
}
