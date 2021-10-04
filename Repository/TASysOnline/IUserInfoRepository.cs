using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface IUserInfoRepository : IRepository<UserInfoTable>
    {
        public Task<UserInfoTable> FindUserInfoByUserAccountId(Guid id);
    }
}
