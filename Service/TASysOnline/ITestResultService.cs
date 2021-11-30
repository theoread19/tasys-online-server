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

        public Task<FilterResponse<List<TestResultResponse>>> FilterTestResultBy(Filter filterRequest, string route);

        public Task<IEnumerable<TestResultResponse>> GetTestResultByTestIdAndUserIdAsync(Guid userId, Guid testId);
    }
}
