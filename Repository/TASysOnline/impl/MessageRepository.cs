﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class MessageRepository : BaseRepository<MessageTable>, IMessageRepository
    {
        public TASysOnlineContext _context;

        public MessageRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<List<MessageTable>> FindMessageBySenderIdAnRecipientId(Guid senderId, Guid recipientId)
        {
            var tables = await this._context.MessageTables.Where(w => w.SenderId.Equals(senderId)
                                                            && w.RecipientId.Equals(recipientId)).ToListAsync();
            return tables;
        }
    }
}
