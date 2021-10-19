using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class UserInfoRepository : BaseRepository<UserInfoTable>, IUserInfoRepository
    {
        private TASysOnlineContext _context;
        public UserInfoRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<UserInfoTable> FindUserInfoByUserAccountId(Guid id)
        {
            try
            {
                return await this._context.UserInfoTables.Where(w => w.UserAccountId == id).FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
