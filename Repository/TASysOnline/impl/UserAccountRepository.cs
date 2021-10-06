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
            return await this._context.UserAccountTables.Where(w => w.Username!.Equals(username)).FirstOrDefaultAsync();
        }

        public async Task<UserAccountTable> GetUserAccountEagerLoadCourse(Guid userId)
        {
            try
            {
                var table = await this._context.UserAccountTables.Where(w => w.Id == userId)
                                                .Include(i => i.CoursesOfInstrucor)
                                                    .ThenInclude(ti => ti.Schedules)
                                                .Include(i => i.CoursesOfLearner)
                                                    .ThenInclude(ti => ti.Schedules)
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
