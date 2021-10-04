using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface IMessageRepository : IRepository<MessageTable>
    {
        public Task<List<MessageTable>> FindMessageBySenderIdAnRecipientId(Guid senderId, Guid recipientId);
    }
}
