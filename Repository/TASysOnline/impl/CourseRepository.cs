using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class CourseRepository : BaseRepository<CourseTable>, ICourseRepository
    {
        public TASysOnlineContext _context;

        public CourseRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<CourseTable> FindByIdAsyncEagerLoad(Guid id)
        {
            return await this._context.CourseTables.Where(w => w.Id == id).Include(i => i.Schedules).FirstOrDefaultAsync();
        }

        public async Task<CourseTable> FindByNameAsync(string name)
        {
            return await this._context.CourseTables.Where(w => w.Name!.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<List<CourseTable>> GetCourseTablesEagerLoadScheduleAsync()
        {
            return await this._context.CourseTables.Include(i => i.Schedules).ToListAsync();
        }
    }
}
