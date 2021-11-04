using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class UserAccountRepository : BaseRepository<UserAccountTable>, IUserAccountRepository 
    {
        private TASysOnlineContext _context;
        public UserAccountRepository() : base (new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task AddCourseToLeaner(Guid userId, CourseTable course)
        {
            var table = await this._context.UserAccountTables.Where(w => w.Id == userId).FirstOrDefaultAsync();
            table.CoursesOfLearner.Add(course);
            await this._context.SaveChangesAsync();
        }

        public async Task<int> CountByRole(Guid roleId)
        {
            try
            {
                return await this._context.UserAccountTables.Where(w => w.RoleId == roleId).CountAsync();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<UserAccountTable> FindByUsernameAsync(string username)
        {
            try
            {
                return await this._context.UserAccountTables.Where(w => w.Username!.Equals(username)).FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserAccountTable> GetUserAccountEagerLoadCourse(Guid userId)
        {
            try
            {
                var table = await this._context.UserAccountTables.Where(w => w.Id == userId)
                                                .Include(i => i.CoursesOfInstrucor)
                                                .Include(i => i.CoursesOfLearner)
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
