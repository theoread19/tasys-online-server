using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class DiscountRepository : BaseRepository<DiscountTable>, IDiscountRepository
    {
        private TASysOnlineContext _context;

        public DiscountRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<DiscountTable> FindDiscountTabeByCourseId(Guid courseId)
        {
            var table = await this._context.DiscountTables.Where(w => w.CourseId == courseId).FirstOrDefaultAsync();
            return table;
        }
    }
}
