using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface ITestResultRepository : IRepository<TestResultTable>
    {
        public Task<int> CountTestResultByUserIdAndTestId(Guid userId, Guid testId);
    }
}
