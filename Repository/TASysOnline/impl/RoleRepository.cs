using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class RoleRepository : BaseRepository<RoleTable>, IRoleRepository
    {
        private TASysOnlineContext _context;

        public RoleRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<RoleTable> GetRoleByName(string name)
        {
            try
            {
                return await this._context.RoleTables.Where(w => w.Name == name).FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
