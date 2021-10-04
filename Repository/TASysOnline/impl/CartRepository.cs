using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class CartRepository : BaseRepository<CartTable>, ICartRepository
    {
        public TASysOnlineContext _context;

        public CartRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }
    }
}
