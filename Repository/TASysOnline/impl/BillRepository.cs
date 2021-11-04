using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class BillRepository : BaseRepository<BillTable>, IBillRepository
    {
        public TASysOnlineContext _context;

        public BillRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task AddCourseToBill(Guid billId, CourseTable courseTable)
        {
            var table = await this._context.BillTables.Where(w => w.Id == billId).FirstOrDefaultAsync();
            table.CourseTables.Add(courseTable);
            await this._context.SaveChangesAsync();
        }

        public async Task<BillTable> GetByIdEagerLoad(Guid Id)
        {
            try
            {
                var table = await this._context.BillTables.Where(w => w.Id == Id)
                                .Include(i => i.CourseTables)
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
