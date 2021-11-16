using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Service.Paging;
using TASysOnlineProject.Table;
using TASysOnlineProject.Utils;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class CourseService : ICourseService
    {

        private ICourseRepository _courseRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        private readonly ISubjectService _subjectService;

        public CourseService(ICourseRepository courseRepository, 
                            IUriService uriService, 
                            IMapper mapper,
                            ISubjectService subjectService)
        {
            this._courseRepository = courseRepository;
            this._uriService = uriService;
            this._mapper = mapper;
            this._subjectService = subjectService;
        }

        public async Task<int> CountAsync()
        {
            return await this._courseRepository.CountAsync();
        }

        public async Task<Response> CreateCourseAsync(CourseRequest courseRequest)
        {

            var subject = await this._subjectService.FindById(courseRequest.SubjectId);

            if (subject.StatusCode == StatusCodes.Status404NotFound)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Subject not found!" };
            }

            var result = await this._courseRepository.FindByNameAsync(courseRequest.Name);

            if (result != null)
            {
                return new Response { StatusCode = StatusCodes.Status500InternalServerError, ResponseMessage = "Course is Exist!" };
            }

            var table = this._mapper.Map<CourseTable>(courseRequest);
            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();
            await this._courseRepository.InsertAsync(table);

            await this._courseRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Course was created!" };
        }

        public async Task<Response> DeleteAllCourse()
        {
            await this._courseRepository.DeleteAllAsyn();
            await this._courseRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all course successfully!"
            };
        }

        public async Task<Response> DeleteCourse(Guid[] courseId)
        {
            for(var i = 0; i < courseId.Length; i++)
            {
                await this._courseRepository.DeleteAsync(courseId[i]);
            }

            await this._courseRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete course successfully!"
            };
        }

        public async Task<FilterResponse<List<CourseResponse>>> FilterCourseBy(Filter filterRequest, string route)
        {   
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._courseRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CourseResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._courseRepository.GetCourseTablesEagerLoadAsync();

            var filterDatas = FilterUtils.Filter(validFilter, tables);

            var pageData = this._mapper.Map<List<CourseTable>, List<CourseResponse>>(filterDatas);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CourseResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<CourseResponse> FindByNameAsync(string name)
        {
            var result = await this._courseRepository.FindByNameAsync(name);

            if (result == null)
            {
                return new CourseResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ResponseMessage = "Course not Found!"
                };
            }

            var response = this._mapper.Map<CourseResponse>(result);

            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Course is Found!";
            return response;
        }

        public async Task<IEnumerable<CourseResponse>> GetAllCourseAsync()
        {
            var tables = await this._courseRepository.GetAllAsync();

            var responses = this._mapper.Map<List<CourseTable>, List<CourseResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<CourseResponse>>> GetAllCoursePagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._courseRepository.CountAsync();

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._courseRepository.GetCourseTablesEagerLoadAsync();

            var PagingDatas = PagedUtil.Pagination<CourseTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<CourseTable>, List<CourseResponse>>(PagingDatas);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CourseResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<CourseResponse> GetCourseById(Guid id)
        {
            var table = await this._courseRepository.FindByIdAsyncEagerLoad(id);

            if (table == null)
            {
                return new CourseResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Course not found!" };
            }


            var response = this._mapper.Map<CourseResponse>(table);
            response.TotalLesson = table.LessonTables.Count;
            response.TotalTest = table.Tests.Count;
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find course successfully";
            return response;
        }

        public async Task<SearchResponse<List<CourseResponse>>> SearchCourseBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._courseRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CourseResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._courseRepository.GetCourseTablesEagerLoadAsync();

            var SearchData = SearchUtils.Search<CourseTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<CourseTable>, List<CourseResponse>>(SearchData);
            var pagedReponse = PaginationHelper.CreatePagedReponse<CourseResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateCourse(CourseRequest courseRequest, AccountAuthorInfo accountAuthorInfo)
        {
            var table = await this._courseRepository.FindByIdAsync(courseRequest.Id);

            if (table == null)
            {
                return new CourseResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Course not found!" };
            }

            if (accountAuthorInfo.Role == Roles.Admin)
            {
                table.Duration = courseRequest.Duration;
                table.InstructorId = courseRequest.InstructorId;
                table.MaxSlot = courseRequest.MaxSlot;
                table.SubjectId = courseRequest.SubjectId;
                table.Summary = courseRequest.Summary;
                table.Name = courseRequest.Name;
                table.Cost = courseRequest.Cost;
                table.AvailableSlot = courseRequest.AvailableSlot;
            }
            else if (table.InstructorId != accountAuthorInfo.Id)
            {             
                return new CourseResponse { StatusCode = StatusCodes.Status403Forbidden, ResponseMessage = "Invalid access data!" };
            }

            table.ModifiedDate = DateTime.UtcNow;
            table.Description = courseRequest.Description;

            await this._courseRepository.UpdateAsync(table);
            await this._courseRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update course successfully!" };
        }

        public async Task<FilterSearchResponse<List<CourseResponse>>> FilterSearchCourseBy(FilterSearch filterSearchRequest, string route)
        {
            var validFilter = new FilterSearch(filterSearchRequest.PageNumber, filterSearchRequest.PageSize, filterSearchRequest.SortBy!, filterSearchRequest.Order!, filterSearchRequest.FilterValue!, filterSearchRequest.FilterProperty!, filterSearchRequest.SearchValue!, filterSearchRequest.SearchProperty!);

            var data = await this._courseRepository.GetCourseTablesEagerLoadAsync();

            var filterSearchData = FilterSearchUtil.FilterSearch<CourseTable>(filterSearchRequest, data);

            var totalData = filterSearchData.Count;

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CourseResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var pageData = this._mapper.Map<List<CourseTable>, List<CourseResponse>>(filterSearchData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CourseResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }
    }
}
