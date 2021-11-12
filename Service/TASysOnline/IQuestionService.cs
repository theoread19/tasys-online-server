using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IQuestionService
    {
        public Task<IEnumerable<QuestionResponse>> GetAllQuestionAsync();

        public Task<PageResponse<List<QuestionResponse>>> GetAllQuestionPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateQuestionAsync(QuestionRequest questionRequest);

        public Task<QuestionResponse> GetQuestionById(Guid id);

        public Task<Response> UpdateQuestion(QuestionRequest questionRequest);

        public Task<Response> DeleteQuestion(Guid[] questionId);

        public Task<FilterResponse<List<QuestionResponse>>> FilterQuestionBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<QuestionResponse>>> SearchQuestionBy(Search searchRequest, string route);

        public Task<Response> DeleteAllQuestion();

        public Task<IEnumerable<QuestionResponse>> FindQuestionByTestId(Guid testId);

        public Task<FilterSearchResponse<List<QuestionResponse>>> FilterSearchQuestionBy(FilterSearch filterSearchRequest, string route);
    }
}
