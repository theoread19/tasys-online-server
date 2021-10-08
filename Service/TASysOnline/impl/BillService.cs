﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BillService : IBillService
    {
        private IBillRepository _billRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        private readonly ICourseService _courseService;

        private readonly ICartService _cartService;

        public BillService(IBillRepository BillRepository, 
                            IUriService uriService, 
                            IMapper mapper, 
                            ICourseService courseService,
                            ICartService cartService)
        {
            this._billRepository = BillRepository;
            this._uriService = uriService;
            this._mapper = mapper;
            this._courseService = courseService;
            this._cartService = cartService;
        }

        public async Task<int> CountAsync()
        {
            return await this._billRepository.CountAsync();
        }

        public async Task<Response> CreateBillAsync(BillRequest BillRequest)
        {

            var table = this._mapper.Map<BillTable>(BillRequest);

            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();
            var bill = await this._billRepository.InsertAsync(table);
            foreach(var courseRequest in BillRequest.CourseRequests)
            {
                bill.CourseTables.Add(this._mapper.Map<CourseTable>(courseRequest));
                await this._courseService.AddLeanersAsync(BillRequest.UserAccountId, courseRequest.Id);
            }
            
            await this._billRepository.SaveAsync();

            await this._cartService.RemoveAllCourseFromCart(BillRequest.UserAccountId);

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Bill was created!" };
        }

        public async Task<Response> DeleteAllBill()
        {
            await this._billRepository.DeleteAllAsyn();
            await this._billRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all Bill successfully!"
            };
        }

        public async Task<Response> DeleteBill(Guid[] BillId)
        {
            for (var i = 0; i < BillId.Length; i++)
            {
                await this._billRepository.DeleteAsync(BillId[i]);
            }

            await this._billRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Bill successfully!"
            };
        }

        public async Task<FilterResponse<List<BillResponse>>> FilterBillBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._billRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<BillResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._billRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<BillTable>, List<BillResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<BillResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<BillResponse> FindByNameAsync(string name)
        {
            /*            var result = await this._BillRepository.FindByNameAsync(name);

                        if (result == null)
                        {
                            return new BillResponse
                            {
                                StatusCode = StatusCodes.Status404NotFound,
                                ResponseMessage = "Bill not Found!"
                            };
                        }

                        var response = this._mapper.Map<BillResponse>(result);

                        response.StatusCode = StatusCodes.Status200OK;
                        response.ResponseMessage = "Bill is Found!";
                        return response;*/
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BillResponse>> GetAllBillAsync()
        {
            var tables = await this._billRepository.GetAllAsync();

            var responses = this._mapper.Map<List<BillTable>, List<BillResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<BillResponse>>> GetAllBillPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._billRepository.CountAsync();

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<BillResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "No data!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._billRepository.GetAllPadingAsync(validFilter);

            if (tables == null)
            {
                var reponse = PaginationHelper.CreatePagedReponse<BillResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "Column name inlvaid";
                return reponse;
            }

            var pageData = this._mapper.Map<List<BillTable>, List<BillResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<BillResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<BillResponse> GetBillById(Guid id)
        {
            var table = await this._billRepository.FindByIdAsync(id);
            var response = this._mapper.Map<BillResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Bill successfully";
            return response;
        }

        public async Task<SearchResponse<List<BillResponse>>> SearchBillBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._billRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<BillResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._billRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<BillTable>, List<BillResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<BillResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }
    }
}
