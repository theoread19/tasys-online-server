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

        public async Task<TestResultTable> FindTestResultByIdEagerLoad(Guid Id)
        {
            try
            {
                var table = await this._context.TestResultTables.Where(w => w.Id == Id).Include(i => i.UserAccount).FirstOrDefaultAsync();
                return table;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<TestResultTable>> GetAllTestResultTablesEagerLoad()
        {
            var tables = await this._context.TestResultTables.Include(i => i.UserAccount).ToListAsync();
            return tables;
        }

        public async Task<List<TestResultTable>> GetTestResultByUserIdAndTestId(Guid userId, Guid testId)
        {
            try
            {
                var tables = await this._context.TestResultTables
                    .Where(w => w.TestId == testId)
                    .Where(w => w.UserAccountId == userId)
                    .ToListAsync();

                return tables;
            }
            catch
            {
                return new List<TestResultTable>();
            }
        }
    }
}
