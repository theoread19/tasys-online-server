using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface ICourseService
    {
        public Task<IEnumerable<CourseResponse>> GetAllCourseAsync();

        public Task<PageResponse<List<CourseResponse>>> GetAllCoursePagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateCourseAsync(CourseRequest courseRequest);

        public Task<CourseResponse> FindByNameAsync(string name);

        public Task<CourseResponse> GetCourseById(Guid id);

        public Task<int> CountAsync();

        public Task<int> CountLeanerOfCourse(Guid courseId);

        public Task<Response> UpdateCourse(CourseRequest courseRequest);

        public Task<Response> DeleteCourse(Guid[] courseId);

        public Task<FilterResponse<List<CourseResponse>>> FilterCourseBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<CourseResponse>>> SearchCourseBy(Search searchRequest, string route);

        public Task<Response> DeleteAllCourse();

        public Task AddLeanersAsync(Guid leanerId, Guid courseId);

        public Task GenerateData();
    }
}
