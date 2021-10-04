using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class TestResultRepository : BaseRepository<TestResultTable>, ITestResultRepository
    {
        public TASysOnlineContext _context;

        public TestResultRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<int> CountTestResultByUserIdAndTestId(Guid userId, Guid testId)
        {
            var count = await this._context.TestResultTables
                                .Where(w => w.TestId == testId)
                                .Where(w => w.UserAccountId == userId)
                                .CountAsync();
            return count;
        }
    }
}
