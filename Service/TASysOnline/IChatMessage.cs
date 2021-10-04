using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IChatMessage
    {
        public Task<PageResponse<List<MessageResponse>>> GetDiscussionOfChat(DiscussionRequest discussionRequest, string route);

        //public Task SendMessage()
    }
}
