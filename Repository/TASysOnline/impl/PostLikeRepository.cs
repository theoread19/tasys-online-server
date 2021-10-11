using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class PostLikeRepository : BaseRepository<PostLikeTable> ,IPostLikeRepository
    {
        public TASysOnlineContext _context;

        public PostLikeRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<PostLikeTable> FindPostLikeByPostIdAndUserId(Guid postId, Guid userId)
        {
            try
            {
                var table = await this._context.PostLikeTables
                                                .Where(w => (w.UserAccountId == userId && w.PostId == postId))
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
