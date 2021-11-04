using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IUserAccountService
    {
        public Task<Response> CreateUserAsync(UserAccountRequest userAccountRequest);

        public Task<IEnumerable<UserAccountResponse>> GetAllUserAccountAsync();

        public Task<AuthorUserResponse?> FindByUsernameForAuthorAsync(string username);

        public Task<UserAccountResponse> FindByNameAsync(string username);

        public Task<int> CountByRoleIdAsync(Guid roleId);

        public Task<Response> ChangeUserAccountPasswordAsync(Guid userId, ChangePasswordRequest changePasswordRequest);

        public Task<UserAccountResponse> FindByIdAsync(Guid id);

        public Task<Response> UpdateUserAccount(UserAccountRequest userAccountRequest, AccountAuthorInfo accountAuthorInfo);

        public Task<Response> DeleteUserAccount(Guid[] userAccountId);

        public Task<FilterResponse<List<UserAccountResponse>>> FilterUserAccountBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<UserAccountResponse>>> SearchUserAccountBy(Search searchRequest, string route);

        public Task<FilterSearchResponse<List<UserAccountResponse>>> FilterSearchUserAccountBy(FilterSearch searchRequest, string route);

        public Task<Response> DeleteAllUserAccount();

        public Task<PageResponse<List<UserAccountResponse>>> GetAllUserAccountPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> BlockUserAccount(Guid userId);

        public Task<SearchResponse<List<CourseResponse>>> SearchCourseOfLearnerBy(Search searchRequest, string route, AccountAuthorInfo accountAuthorInfo, Guid userId);

        public Task<Response> AddCourseToLearner(Guid userId, List<CourseRequest> courseRequests);
    }
}
