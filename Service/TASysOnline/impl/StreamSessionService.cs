using AutoMapper;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    public class StreamSessionService : IStreamSessionService
    {
        private IStreamSessionRepository _StreamSessionRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        public StreamSessionService(IStreamSessionRepository streamSessionRepository, IUriService uriService, IMapper mapper)
        {
            this._StreamSessionRepository = streamSessionRepository;
            this._uriService = uriService;
            this._mapper = mapper;
        }

        public async Task<int> CountAsync()
        {
            return await this._StreamSessionRepository.CountAsync();
        }

        public async Task<Response> CreateStreamSessionAsync(StreamSessionRequest streamSessionRequest)
        {

            var table = this._mapper.Map<StreamSessionTable>(streamSessionRequest);

            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();

            await this._StreamSessionRepository.InsertAsync(table);
            await this._StreamSessionRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "StreamSession was created!" };
        }

        public async Task<Response> DeleteAllStreamSession()
        {
            await this._StreamSessionRepository.DeleteAllAsyn();
            await this._StreamSessionRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all StreamSession successfully!"
            };
        }

        public async Task<Response> DeleteStreamSession(Guid[] StreamSessionId)
        {
            for (var i = 0; i < StreamSessionId.Length; i++)
            {
                await this._StreamSessionRepository.DeleteAsync(StreamSessionId[i]);
            }

            await this._StreamSessionRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete StreamSession successfully!"
            };
        }

        public async Task<MemoryStream> ExportDBToExcel(List<UserAccountAuthRequest> learner)
        {
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(learner, true);
                package.Save();
            }
            stream.Position = 0;
            return stream;
        }

        public async Task<FilterSearchResponse<List<StreamSessionResponse>>> FilterSearchStreamSessionBy(FilterSearch filterSearchRequest, string route)
        {
            var validFilter = new FilterSearch(filterSearchRequest.PageNumber, filterSearchRequest.PageSize, filterSearchRequest.SortBy!, filterSearchRequest.Order!, filterSearchRequest.FilterValue!, filterSearchRequest.FilterProperty!, filterSearchRequest.SearchValue!, filterSearchRequest.SearchProperty!);

            var data = await this._StreamSessionRepository.GetAllStreamSessionEagerLoadAsync();

            var filterSearchData = FilterSearchUtil.FilterSearch<StreamSessionTable>(filterSearchRequest, data);

            var totalData = filterSearchData.Count;

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<StreamSessionResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var pageData = this._mapper.Map<List<StreamSessionTable>, List<StreamSessionResponse>>(filterSearchData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<StreamSessionResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<FilterResponse<List<StreamSessionResponse>>> FilterStreamSessionBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._StreamSessionRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<StreamSessionResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._StreamSessionRepository.GetAllStreamSessionEagerLoadAsync();

            var filterData = FilterUtils.Filter<StreamSessionTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<StreamSessionTable>, List<StreamSessionResponse>>(filterData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<StreamSessionResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }
        public async Task<IEnumerable<StreamSessionResponse>> GetAllStreamSessionAsync()
        {
            var tables = await this._StreamSessionRepository.GetAllAsync();

            var responses = this._mapper.Map<List<StreamSessionTable>, List<StreamSessionResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<StreamSessionResponse>>> GetAllStreamSessionPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._StreamSessionRepository.CountAsync();

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._StreamSessionRepository.GetAllStreamSessionEagerLoadAsync();

            var pagedData = PagedUtil.Pagination<StreamSessionTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<StreamSessionTable>, List<StreamSessionResponse>>(pagedData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<StreamSessionResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<StreamSessionResponse>> GetComingStreamSessionAsync(DateTime now)
        {
            var tables = await this._StreamSessionRepository.GetComingStreamSessionEagerLoadAsync(now);

            var responses = this._mapper.Map<List<StreamSessionTable>, List<StreamSessionResponse>>(tables);

            return responses;
        }

        public async Task<StreamSessionResponse> GetStreamSessionById(Guid id)
        {
            var table = await this._StreamSessionRepository.FindByIdAsync(id);

            if (table == null)
            {
                return new StreamSessionResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Stream not found!" };
            }

            var response = this._mapper.Map<StreamSessionResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find StreamSession successfully";
            return response;
        }

        public async Task<SearchResponse<List<StreamSessionResponse>>> SearchStreamSessionBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._StreamSessionRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<StreamSessionResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._StreamSessionRepository.GetAllStreamSessionEagerLoadAsync();

            var searchData = SearchUtils.Search<StreamSessionTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<StreamSessionTable>, List<StreamSessionResponse>>(searchData);
            var pagedReponse = PaginationHelper.CreatePagedReponse<StreamSessionResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateStreamSession(StreamSessionRequest streamSessionRequest)
        {

            var table = await this._StreamSessionRepository.FindByIdAsync(streamSessionRequest.Id);

            if (table == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Stream not found!" };
            }

            table.ModifiedDate = DateTime.UtcNow;
            table.EndTime = streamSessionRequest.EndTime;
            table.StartTime = streamSessionRequest.StartTime;

            await this._StreamSessionRepository.UpdateAsync(table);
            await this._StreamSessionRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update StreamSession successfully!" };
        }
    }
}
