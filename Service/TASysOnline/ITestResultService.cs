using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface ITestResultService
    {
        public Task<TestResultResponse> CalculateTestResult(DoTestRequest doTestRequest);

        public Task<IEnumerable<TestResultResponse>> GetAllTestResultAsync();

        public Task<PageResponse<List<TestResultResponse>>> GetAllTestResultPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateTestResultAsync(TestResultRequest testResultRequest);

        public Task<TestResultResponse> GetTestResultById(Guid id);

        public Task<int> CountAsync();

        public Task<Response> DeleteTestResult(Guid[] testResultId);

        public Task<FilterResponse<List<TestResultResponse>>> FilterTestResultBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<TestResultResponse>>> SearchTestResultBy(Search searchRequest, string route);

        public Task<Response> DeleteAllTestResult();
    }
}
