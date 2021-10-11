using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface IPostLikeRepository : IRepository<PostLikeTable>
    {
        public Task<PostLikeTable> FindPostLikeByPostIdAndUserId(Guid postId, Guid userId);
    }
}
