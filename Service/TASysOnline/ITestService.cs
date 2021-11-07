using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface ITestService
    {
        public Task<IEnumerable<TestResponse>> GetAllTestAsync();

        public Task<PageResponse<List<TestResponse>>> GetAllTestPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateTestAsync(TestRequest testRequest);

        public Task<TestResponse> GetTestById(Guid id);

        public Task<Response> UpdateTest(TestRequest testRequest);

        public Task<Response> DeleteTest(Guid[] testId);

        public Task<FilterResponse<List<TestResponse>>> FilterTestBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<TestResponse>>> SearchTestBy(Search searchRequest, string route);

        public Task<Response> DeleteAllTest();

        public Task<FilterSearchResponse<List<TestResponse>>> FilterSearchTestBy(FilterSearch filterSearchRequest, string route);
    }
}
