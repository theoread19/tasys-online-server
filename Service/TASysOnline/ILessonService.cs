using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface ILessonService
    {
        public Task<IEnumerable<LessonResponse>> GetAllLessonAsync();

        public Task<PageResponse<List<LessonResponse>>> GetAllLessonPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateLessonAsync(LessonRequest lessonRequest);

        public Task<LessonResponse> GetLessonById(Guid id);

        public Task<Response> UpdateLesson(LessonRequest lessonRequest);

        public Task<Response> DeleteLesson(Guid[] lessonId);

        public Task<FilterResponse<List<LessonResponse>>> FilterLessonBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<LessonResponse>>> SearchLessonBy(Search searchRequest, string route);

        public Task<FilterSearchResponse<List<LessonResponse>>> FilterSearchLessonBy(FilterSearch filterSearchRequest, string route);

        public Task<Response> DeleteAllLesson();
    }
}
