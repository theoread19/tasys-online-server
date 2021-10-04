using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class PostRepository : BaseRepository<PostTable>, IPostRepository
    {
        private TASysOnlineContext _context;

        public PostRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

    }
}
