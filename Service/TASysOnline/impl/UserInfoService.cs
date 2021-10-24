using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Service.Paging;
using TASysOnlineProject.Table;
using TASysOnlineProject.Utils;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public UserInfoService(IUserInfoRepository userInfoRepository, IUriService uriService, IMapper mapper)
        {
            this._userInfoRepository = userInfoRepository;
            this._uriService = uriService;
            this._mapper = mapper;
        }
        public async Task<int> CountAsync()
        {
            return await this._userInfoRepository.CountAsync();
        }

        public async Task<Response> CreateUserInfoAsync(UserInfoRequest userInfoRequest)
        {

            var table = this._mapper.Map<UserInfoTable>(userInfoRequest);
            table.CreatedDate = DateTime.UtcNow;

            await this._userInfoRepository.InsertAsync(table);
            await this._userInfoRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "User information was created!" };
        }

        public async Task<Response> DeleteAllUserInfo()
        {
            await this._userInfoRepository.DeleteAllAsyn();
            await this._userInfoRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all user information successfully!"
            };
        }

        public async Task<Response> DeleteUserInfo(Guid[] UserInfoId)
        {
            for (var i = 0; i < UserInfoId.Length; i++)
            {
                await this._userInfoRepository.DeleteAsync(UserInfoId[i]);
            }

            await this._userInfoRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete user information successfully!"
            };
        }

        public async Task<FilterResponse<List<UserInfoResponse>>> FilterUserInfoBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._userInfoRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<UserInfoResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._userInfoRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<UserInfoTable>, List<UserInfoResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<UserInfoResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<UserInfoResponse>> GetAllUserInfoAsync()
        {
            var tables = await this._userInfoRepository.GetAllAsync();

            var responses = this._mapper.Map<List<UserInfoTable>, List<UserInfoResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<UserInfoResponse>>> GetAllUserInfoPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._userInfoRepository.CountAsync();

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<UserInfoResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "No data!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._userInfoRepository.GetAllPadingAsync(validFilter);

            var pageData = this._mapper.Map<List<UserInfoTable>, List<UserInfoResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<UserInfoResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<UserInfoResponse> GetUserInfoById(Guid id)
        {
            var table = await this._userInfoRepository.FindUserInfoByUserAccountId(id);

            if(table == null)
            {
                return new UserInfoResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User info not found!" };
            }

            var response = this._mapper.Map<UserInfoResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find user info successfully";
            return response;
        }

        public async Task<SearchResponse<List<UserInfoResponse>>> SearchUserInfoBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._userInfoRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<UserInfoResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._userInfoRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<UserInfoTable>, List<UserInfoResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<UserInfoResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateUserInfo(UserInfoRequest userInfoRequest)
        {
            var table = await this._userInfoRepository.FindByIdAsync(userInfoRequest.Id);

            if (table == null)
            {
                return new UserInfoResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User info not found!" };
            }

            table.Address = userInfoRequest.Address;
            table.DateOfBirth = userInfoRequest.DateOfBirth;
            table.Email = userInfoRequest.Email;
            table.FullName = userInfoRequest.FullName;
            table.Gender = userInfoRequest.Gender;
            table.Bio = userInfoRequest.Bio;
            table.Phone = userInfoRequest.Phone;
            table.ModifiedDate = DateTime.UtcNow;

            await this._userInfoRepository.UpdateAsync(table);
            await this._userInfoRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update user account successfully!" };
        }
    }
}
