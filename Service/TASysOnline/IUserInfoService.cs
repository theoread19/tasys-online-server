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

        public Task<PageResponse<List<UserInfoResponse>>> GetAllUserInfoPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateUserInfoAsync(UserInfoRequest userInfoRequest);

        public Task<int> CountAsync();

        public Task<UserInfoResponse> GetUserInfoById(Guid id);

        public Task<Response> UpdateUserInfo(UserInfoRequest UserInfoRequest);

        public Task<Response> DeleteUserInfo(Guid[] UserInfoId);

        public Task<FilterResponse<List<UserInfoResponse>>> FilterUserInfoBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<UserInfoResponse>>> SearchUserInfoBy(Search searchRequest, string route);

        public Task<Response> DeleteAllUserInfo();
    }
}
