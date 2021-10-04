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
    }
}
