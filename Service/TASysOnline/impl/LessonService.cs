using AutoMapper;
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
    public class LessonService : ILessonService
    {
        private ILessonRepository _LessonRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        public LessonService(ILessonRepository LessonRepository, IUriService uriService, IMapper mapper)
        {
            this._LessonRepository = LessonRepository;
            this._uriService = uriService;
            this._mapper = mapper;
        }

        public async Task<Response> CreateLessonAsync(LessonRequest lessonRequest)
        {

            var table = this._mapper.Map<LessonTable>(lessonRequest);

            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();
            await this._LessonRepository.InsertAsync(table);
            await this._LessonRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Lesson was created!" };
        }

        public async Task<Response> DeleteAllLesson()
        {
            await this._LessonRepository.DeleteAllAsyn();
            await this._LessonRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all Lesson successfully!"
            };
        }

        public async Task<Response> DeleteLesson(Guid[] LessonId)
        {
            for (var i = 0; i < LessonId.Length; i++)
            {
                await this._LessonRepository.DeleteAsync(LessonId[i]);
            }

            await this._LessonRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Lesson successfully!"
            };
        }

        public async Task<FilterResponse<List<LessonResponse>>> FilterLessonBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._LessonRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<LessonResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._LessonRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<LessonTable>, List<LessonResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<LessonResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<FilterSearchResponse<List<LessonResponse>>> FilterSearchLessonBy(FilterSearch filterSearchRequest, string route)
        {
            var validFilter = new FilterSearch(filterSearchRequest.PageNumber, 
                                        filterSearchRequest.PageSize, 
                                        filterSearchRequest.SortBy!, 
                                        filterSearchRequest.Order!, 
                                        filterSearchRequest.FilterValue!, 
                                        filterSearchRequest.FilterProperty!, 
                                        filterSearchRequest.SearchValue!, 
                                        filterSearchRequest.SearchProperty!);

            var data = await this._LessonRepository.GetAllAsync();

            var filterSearchData = FilterSearchUtil.FilterSearch<LessonTable>(validFilter, data);

            var totalData = filterSearchData.Count;

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<LessonResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var pageData = this._mapper.Map<List<LessonTable>, List<LessonResponse>>(filterSearchData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<LessonResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<LessonResponse>> GetAllLessonAsync()
        {
            var tables = await this._LessonRepository.GetAllAsync();

            var responses = this._mapper.Map<List<LessonTable>, List<LessonResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<LessonResponse>>> GetAllLessonPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._LessonRepository.CountAsync();

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._LessonRepository.GetAllPadingAsync(validFilter);

            var pageData = this._mapper.Map<List<LessonTable>, List<LessonResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<LessonResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<LessonResponse> GetLessonById(Guid id)
        {
            var table = await this._LessonRepository.FindByIdAsync(id);

            if (table == null)
            {
                return new LessonResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Lesson not found!" };
            }

            var response = this._mapper.Map<LessonResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Lesson successfully";
            return response;
        }

        public async Task<SearchResponse<List<LessonResponse>>> SearchLessonBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._LessonRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<LessonResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._LessonRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<LessonTable>, List<LessonResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<LessonResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateLesson(LessonRequest lessonRequest)
        {
            var table = await this._LessonRepository.FindByIdAsync(lessonRequest.Id);

            if (table == null)
            {
                return new LessonResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Lesson not found!" };
            }

            table.Name = lessonRequest.Name;
            table.ModifiedDate = DateTime.UtcNow;
            table.Description = lessonRequest.Description;
            table.BackText = lessonRequest.BackText;
            table.FrontText = lessonRequest.FrontText;
            table.CourseId = lessonRequest.CourseId;

            await this._LessonRepository.UpdateAsync(table);
            await this._LessonRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Lesson successfully!" };
        }
    }
}
