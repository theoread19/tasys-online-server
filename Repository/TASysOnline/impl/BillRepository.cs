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
    }
}
