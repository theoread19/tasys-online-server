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
    public class CourseService : ICourseService
    {

        private ICourseRepository _courseRepository;

        private readonly IUserAccountService _userAccountService;

        private IUriService _uriService;

        private IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IUriService uriService, IMapper mapper, IUserAccountService userAccountService)
        {
            this._courseRepository = courseRepository;
            this._uriService = uriService;
            this._mapper = mapper;
            this._userAccountService = userAccountService;
        }

        public async Task AddLeanersAsync(Guid leanerId, Guid courseId)
        {

            var courseTable = await this._courseRepository.FindByIdAsync(courseId);
            var user = this._mapper.Map<UserAccountTable>(await this._userAccountService.FindByIdAsync(leanerId));
            courseTable.LearnerAccounts.Add(user);

//            await this._courseRepository.UpdateAsync(courseTable);
            await this._courseRepository.SaveAsync();
        }

        public async Task<int> CountAsync()
        {
            return await this._courseRepository.CountAsync();
        }

        public async Task<Response> CreateCourseAsync(CourseRequest courseRequest)
        {
            var result = await this.FindByNameAsync(courseRequest.Name);

            if (result.StatusCode == StatusCodes.Status200OK)
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

            var tables = await this._courseRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<CourseTable>, List<CourseResponse>>(tables);

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

            if(totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CourseResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "No data!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._courseRepository.GetAllPadingAsync(validFilter);

            if (tables == null)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CourseResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "Column name inlvaid";
                return reponse;
            }

            var pageData = this._mapper.Map<List<CourseTable>, List<CourseResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CourseResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<CourseResponse> GetCourseById(Guid id)
        {
            var table = await this._courseRepository.FindByIdAsync(id);
            var response = this._mapper.Map<CourseResponse>(table);
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

            var tables = await this._courseRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<CourseTable>, List<CourseResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<CourseResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateCourse(CourseRequest courseRequest)
        {
            var table = await this._courseRepository.FindByIdAsync(courseRequest.Id);
            table.Name = courseRequest.Name;
            table.Cost = courseRequest.Cost;
            table.AvailableSlot = courseRequest.AvailableSlot;
            table.ModifiedDate = DateTime.UtcNow;
            table.Description = courseRequest.Description;
            table.Duration = courseRequest.Duration;
            table.Feedback = courseRequest.Feedback;
            table.Rating = courseRequest.Rating;
            table.RatingCount = courseRequest.RatingCount;
            table.InstructorId = courseRequest.InstructorId;
            table.MaxSlot = courseRequest.MaxSlot;
            table.SubjectId = courseRequest.SubjectId;
            table.Summary = courseRequest.Summary;

            await this._courseRepository.UpdateAsync(table);
            await this._courseRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update course successfully!" };
        }
    }
}
