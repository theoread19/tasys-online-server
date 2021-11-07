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
    public class CurriCulumService : ICurriCulumService
    {
        private ICurriCulumRepository _CurriCulumRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        public CurriCulumService(ICurriCulumRepository CurriCulumRepository, IUriService uriService, IMapper mapper)
        {
            this._CurriCulumRepository = CurriCulumRepository;
            this._uriService = uriService;
            this._mapper = mapper;
        }

        public async Task<Response> CreateCurriCulumAsync(CurriCulumRequest CurriCulumRequest)
        {

            var table = this._mapper.Map<CurriCulumTable>(CurriCulumRequest);

            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();
            await this._CurriCulumRepository.InsertAsync(table);
            await this._CurriCulumRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "CurriCulum was created!" };
        }

        public async Task<Response> DeleteAllCurriCulum()
        {
            await this._CurriCulumRepository.DeleteAllAsyn();
            await this._CurriCulumRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all CurriCulum successfully!"
            };
        }

        public async Task<Response> DeleteCurriCulum(Guid[] CurriCulumId)
        {
            for (var i = 0; i < CurriCulumId.Length; i++)
            {
                await this._CurriCulumRepository.DeleteAsync(CurriCulumId[i]);
            }

            await this._CurriCulumRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete CurriCulum successfully!"
            };
        }

        public async Task<FilterResponse<List<CurriCulumResponse>>> FilterCurriCulumBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._CurriCulumRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CurriCulumResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._CurriCulumRepository.GetAllCurriCulumTablesEagerLoad();
            var filterData = FilterUtils.Filter<CurriCulumTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<CurriCulumTable>, List<CurriCulumResponse>>(filterData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CurriCulumResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<CurriCulumResponse>> GetAllCurriCulumAsync()
        {
            var tables = await this._CurriCulumRepository.GetAllAsync();

            var responses = this._mapper.Map<List<CurriCulumTable>, List<CurriCulumResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<CurriCulumResponse>>> GetAllCurriCulumPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._CurriCulumRepository.CountAsync();

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._CurriCulumRepository.GetAllCurriCulumTablesEagerLoad();

            var pagedData = PagedUtil.Pagination<CurriCulumTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<CurriCulumTable>, List<CurriCulumResponse>>(pagedData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CurriCulumResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<CurriCulumResponse> GetCurriCulumById(Guid id)
        {
            var table = await this._CurriCulumRepository.FindByIdEagerLoad(id);
            var response = this._mapper.Map<CurriCulumResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find CurriCulum successfully";
            return response;
        }

        public async Task<SearchResponse<List<CurriCulumResponse>>> SearchCurriCulumBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._CurriCulumRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CurriCulumResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._CurriCulumRepository.GetAllCurriCulumTablesEagerLoad();

            var searchData = SearchUtils.Search<CurriCulumTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<CurriCulumTable>, List<CurriCulumResponse>>(searchData);
            var pagedReponse = PaginationHelper.CreatePagedReponse<CurriCulumResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateCurriCulum(CurriCulumRequest curriCulumRequest)
        {
            var table = await this._CurriCulumRepository.FindByIdAsync(curriCulumRequest.Id);

            table.Name = curriCulumRequest.Name;
            table.ModifiedDate = DateTime.UtcNow;

            await this._CurriCulumRepository.UpdateAsync(table);
            await this._CurriCulumRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update CurriCulum successfully!" };
        }
    }
}
