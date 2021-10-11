using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class TestRepository : BaseRepository<TestTable>, ITestRepository
    {
        private TASysOnlineContext _context;
        public TestRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<TestTable> FindTestByIdEagerAsync(Guid id)
        {
            try
            {
                var table = await this._context.TestTables
                .Where(w => w.Id == id)
                .Include(i => i.Questions)
                .ThenInclude(t => t.Answers)
                .FirstOrDefaultAsync();
                return table;
            }
            catch
            {
                return null;
            }
        }
    }
}
