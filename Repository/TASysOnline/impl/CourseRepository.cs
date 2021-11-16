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

        public async Task AddLeanerToCourse(UserAccountTable userAccountTable, Guid courseId)
        {
            var table = await this._context.CourseTables.Where(w => w.Id == courseId).FirstOrDefaultAsync();
            table.LearnerAccounts.Add(userAccountTable);
            await this._context.SaveChangesAsync();
        }

        public async Task<int> CountLeanerOfCourse(Guid courseId)
        {
            try
            {
                var table = await this._context.CourseTables.Where(w => w.Id == courseId).Include(i => i.LearnerAccounts).FirstOrDefaultAsync();
                var count = table.LearnerAccounts.Count();
                return count;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<CourseTable> FindByIdAsyncEagerLoad(Guid id)
        {
            try
            {
                return await this._context.CourseTables.Where(w => w.Id == id)
                                                        .Include(i => i.LessonTables)
                                                        .Include(i => i.Tests)
                                                        .Include(i => i.LearnerAccounts)
                                                        .FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<CourseTable> FindByNameAsync(string name)
        {
            return await this._context.CourseTables.Where(w => w.Name!.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<List<CourseTable>> GetCourseTablesEagerLoadAsync()
        {
            return await this._context.CourseTables
                                        .Include(i => i.InstructorAccount)
                                        .ToListAsync();
        }
    }
}
