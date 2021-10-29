using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Data.Requests;
using Microsoft.AspNetCore.Identity;
using TASysOnlineProject.Table.Identity;
using BC = BCrypt.Net.BCrypt;
using TASysOnlineProject.Table;
using Microsoft.AspNetCore.Http;
using TASysOnlineProject.Data;
using TASysOnlineProject.Service.Paging;
using TASysOnlineProject.Utils;
using AutoMapper;
using TASysOnlineProject.Data.Const;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class UserAccountService : IUserAccountService
    {

        private readonly IUserAccountRepository _userAccountRepository;

        private readonly IUserInfoService _userInfoService;

        private readonly IRoleService _roleService;

        private readonly IUriService _uriService;

        private readonly IMapper _mapper;

        private readonly IIdentityService _identityService;

        private readonly ICartRepository _cartRepository;

        public UserAccountService(IUserAccountRepository userAccountRepository, 
                                    IRoleService roleService, 
                                    IUriService uriService, 
                                    IMapper mapper, 
                                    IUserInfoService userInfoService, 
                                    IIdentityService identityService,
                                    ICartRepository cartRepository)
        {
            this._identityService = identityService;
            this._roleService = roleService;
            this._userAccountRepository = userAccountRepository;
            this._uriService = uriService;
            this._mapper = mapper;
            this._userInfoService = userInfoService;
            this._cartRepository = cartRepository;
        }

        public async Task<Response> CreateUserAsync(UserAccountRequest userAccountRequest)
        {

            userAccountRequest.Password = BC.HashPassword(userAccountRequest.Password);
            var table = this._mapper.Map<UserAccountTable>(userAccountRequest);

            table.CreatedDate = DateTime.UtcNow;

            var userTable = await this._userAccountRepository.InsertAsync(table);

            await this._userAccountRepository.SaveAsync();

            var role = await this._roleService.FindByIdAsync(userAccountRequest.RoleId);

            var identity = this._mapper.Map<IdentityUserAccount>(table);

            await this._identityService.CreateIdentityUserAccountAsync(identity, role.Name);

            // Create userinfo
            await this._userInfoService.CreateUserInfoAsync(new UserInfoRequest().Create(userTable.Id));

            // Create cart
            await this._cartRepository.InsertAsync(new CartTable {Id = Guid.NewGuid(), TotalCourse = 0, UserAccountId = userTable.Id });
            await this._cartRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "User was created!" };
        }

        public async Task<AuthorUserResponse?> FindByUsernameForAuthorAsync(string username)
        {
            var table = await this._userAccountRepository.FindByUsernameAsync(username);

            if (table == null)
            {
                return null;
            }

            return new AuthorUserResponse {Id = table.Id, Username = table.Username, RoleId = table.RoleId, Password = table.Password, Status = table.Status, DisplayName = table.DisplayName, ResponseMessage = "User account is found!", StatusCode = StatusCodes.Status200OK };
        }

        public async Task<IEnumerable<UserAccountResponse>> GetAllUserAccountAsync()
        {
            var tables = await this._userAccountRepository.GetAllAsync();

            List<UserAccountResponse> responses = this._mapper.Map<List<UserAccountTable>, List<UserAccountResponse>>(tables);

            return responses;
        }

        public async Task<UserAccountResponse> FindByNameAsync(string username)
        {
            var table = await this._userAccountRepository.FindByUsernameAsync(username);

            if (table == null)
            {
                return new UserAccountResponse {StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User not found!" };
            }

            var reponse = this._mapper.Map<UserAccountResponse>(table);
            reponse.ResponseMessage = "Fetching user successfully!";
            reponse.StatusCode = StatusCodes.Status200OK;
            return reponse;
        }

        public async Task<int> CountAsync()
        {
            return await this._userAccountRepository.CountAsync();
        }

        public async Task<Response> UpdateUserAccount(UserAccountRequest userAccountRequest, AccountAuthorInfo accountAuthorInfo)
        {
            var table = await this._userAccountRepository.FindByIdAsync(userAccountRequest.Id);

            if (table == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User not found!" };
            }

            if (accountAuthorInfo.Role == Roles.Admin)
            {
                var identity = this._mapper.Map<IdentityUserAccount>(table);
                table.RoleId = userAccountRequest.RoleId;
                var role = await this._roleService.FindByIdAsync(userAccountRequest.RoleId);
                await this._identityService.ChangeRoleIdentityUserAccountAsync(identity, role.Name);
            } 
            else if (accountAuthorInfo.Id != userAccountRequest.Id) 
            {
                return new Response { StatusCode = StatusCodes.Status403Forbidden, ResponseMessage = "Invalid access data!" };
            };

            table.DisplayName = userAccountRequest.DisplayName;
            table.ModifiedDate = DateTime.UtcNow;
            await this._userAccountRepository.UpdateAsync(table);
            await this._userAccountRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update user account successfully!" };
        }

        public async Task<Response> DeleteUserAccount(Guid[] userAccountId)
        {
            for (var i = 0; i < userAccountId.Length; i++)
            {
                await this._userAccountRepository.DeleteAsync(userAccountId[i]);
            }

            await this._userAccountRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete user account successfully!"
            };
        }

        public async Task<FilterResponse<List<UserAccountResponse>>> FilterUserAccountBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._userAccountRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<UserAccountResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._userAccountRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<UserAccountTable>, List<UserAccountResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<UserAccountResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<SearchResponse<List<UserAccountResponse>>> SearchUserAccountBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._userAccountRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<UserAccountResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._userAccountRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<UserAccountTable>, List<UserAccountResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<UserAccountResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> DeleteAllUserAccount()
        {
            await this._userAccountRepository.DeleteAllAsyn();
            await this._userAccountRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all user account successfully!"
            };
        }

        public async Task<PageResponse<List<UserAccountResponse>>> GetAllUserAccountPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._userAccountRepository.CountAsync();

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<UserAccountResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "No data!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._userAccountRepository.GetAllPadingAsync(validFilter);

            if (tables == null)
            {
                var reponse = PaginationHelper.CreatePagedReponse<UserAccountResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "Column name inlvaid";
                return reponse;
            }

            var pageData = this._mapper.Map<List<UserAccountTable>, List<UserAccountResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<UserAccountResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<UserAccountResponse> FindByIdAsync(Guid id)
        {
            var table = await this._userAccountRepository.FindByIdAsync(id);

            var reponse = this._mapper.Map<UserAccountResponse>(table);
            reponse.ResponseMessage = "Find user account successfully";
            reponse.StatusCode = StatusCodes.Status200OK;
            return reponse;
        }

        public async Task<Response> ChangeUserAccountPasswordAsync(Guid id, ChangePasswordRequest changePasswordRequest)
        {
            var user = await this._userAccountRepository.FindByIdAsync(id);

            if (user == null)
            {
                return new Response { StatusCode = StatusCodes.Status500InternalServerError, ResponseMessage = "not found user account!" };
            }

            if (!(BC.Verify(changePasswordRequest.OldPassword, user.Password)))
            {
                return new Response { StatusCode = StatusCodes.Status500InternalServerError, ResponseMessage = "wrong password!" };
            }

            user.Password = BC.HashPassword(changePasswordRequest.NewPassword);
            user.ModifiedDate = DateTime.UtcNow;
            await this._userAccountRepository.UpdateAsync(user);
            await this._userAccountRepository.SaveAsync();

            var identity = this._mapper.Map<IdentityUserAccount>(user);

            await this._identityService.UpdateIdentityUserAccountAsync(identity);

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Change password successfully!" };
        }

        public async Task<FilterSearchResponse<List<UserAccountResponse>>> FilterSearchUserAccountBy(FilterSearch filterSearchRequest, string route)
        {
            var validFilter = new FilterSearch(filterSearchRequest.PageNumber, filterSearchRequest.PageSize, filterSearchRequest.SortBy!, filterSearchRequest.Order!, filterSearchRequest.FilterValue!, filterSearchRequest.FilterProperty!, filterSearchRequest.SearchValue!, filterSearchRequest.SearchProperty!);

            var data = await this._userAccountRepository.GetAllAsync();

            var filterSearchData = FilterSearchUtil.FilterSearch<UserAccountTable>(filterSearchRequest, data);

            var totalData = filterSearchData.Count;

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<UserAccountResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var pageData = this._mapper.Map<List<UserAccountTable>, List<UserAccountResponse>>(filterSearchData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<UserAccountResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> BlockUserAccount(Guid userId)
        {
            var user = await this._userAccountRepository.FindByIdAsync(userId);
            user.Status = -1;
            await this._userAccountRepository.UpdateAsync(user);
            await this._userAccountRepository.SaveAsync(); 
            
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Block user account successfully!"
            };
        }

        public async Task<int> CountByRoleIdAsync(Guid roleId)
        {
            return await this._userAccountRepository.CountByRole(roleId);
        }

        public async Task<UserAccountResponse> GetUserAccountEagerLoadCourse(Guid userId)
        {
            var user = await this._userAccountRepository.GetUserAccountEagerLoadCourse(userId);

            var response = this._mapper.Map<UserAccountResponse>(user);

            if (user != null)
            {
                if (user.RoleId.ToString() == Roles.InstructorId)
                {
                    response.CourseResponses = this._mapper.Map<List<CourseTable>, List<CourseResponse>>(user.CoursesOfInstrucor.ToList());
                }
                else if (user.RoleId.ToString() == Roles.LearnerId)
                {
                    response.CourseResponses = this._mapper.Map<List<CourseTable>, List<CourseResponse>>(user.CoursesOfLearner.ToList());
                }

                response.StatusCode = StatusCodes.Status200OK;
                response.ResponseMessage = "Fetching a user successfully!";

                return response;
            }

            return new UserAccountResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User not found!"};
        }

        public async Task<SearchResponse<List<CourseResponse>>> SearchCourseOfLearnerBy(Search searchRequest, string route, AccountAuthorInfo accountAuthorInfo, Guid userId)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            if (userId != accountAuthorInfo.Id)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CourseResponse>(null, validFilter, 0, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status403Forbidden;
                reponse.ResponseMessage = "Invalid access data!";
                return reponse;
            }

            var user = await this._userAccountRepository.GetUserAccountEagerLoadCourse(userId);

            if (user == null)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CourseResponse>(null, validFilter, 0, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "User Not Found!";
                return reponse;
            }

            var course = user.CoursesOfLearner.ToList();

            var totalData = course.Count;

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CourseResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var pageData = this._mapper.Map<List<CourseTable>, List<CourseResponse>>(course);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CourseResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }
    }
}
