using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class CurriCulumRepository : BaseRepository<CurriCulumTable>, ICurriCulumRepository
    {
        public TASysOnlineContext _context;

        public CurriCulumRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<CurriCulumTable> FindByIdEagerLoad(Guid id)
        {
            try
            {
                var table = await this._context.CurriCulumTables.Where(w => w.Id == id).Include(i => i.Course).FirstOrDefaultAsync();
                return table;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<CurriCulumTable>> GetAllCurriCulumTablesEagerLoad()
        {
            var tables = await this._context.CurriCulumTables.Include(i => i.Course).ToListAsync();

            return tables;
        }
    }
}
