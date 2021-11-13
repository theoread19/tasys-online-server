using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface IMessageRepository : IRepository<MessageTable>
    {
        public Task<List<MessageTable>> GetAllMessageEagerLoad();
    }
}
