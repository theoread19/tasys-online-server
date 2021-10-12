using Microsoft.EntityFrameworkCore;
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

        public async Task<List<PostTable>> GetAllPostEagerLoadAsync()
        {
            try
            {
                var tables = await this._context.PostTables
                                                .Include(i => i.PostLikes)
                                                    .ThenInclude(ti => ti.UserAccount)
                                                .Include(i => i.UserAccount)
                                                .ToListAsync();

                return tables;
            }
            catch
            {
                return new List<PostTable>();
            }
        }
    }
}
