using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Service.Paging;
using TASysOnlineProject.Utils;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class ChatService : IChatMessage
    {

        private readonly IMessageService _messageService;
        private readonly IUriService _uriService;

        public ChatService(IMessageService messageService,IUriService uriService)
        {
            this._messageService = messageService;
            this._uriService = uriService;
        }

        public async Task<PageResponse<List<MessageResponse>>> GetDiscussionOfChat(DiscussionRequest discussionRequest, string route)
        {
            var messages = await this._messageService.GetMessageBySenderIdAndRecipientIdAsync(discussionRequest.SenderId, discussionRequest.SenderId);

            var totalMessage = messages.Count();
            var validPaged = new Pagination(discussionRequest.PageNumber, discussionRequest.PageSize, null, null);
            if (totalMessage < 1)
            {
                var reponse = PaginationHelper.CreatePagedReponse<MessageResponse>(null, validPaged, totalMessage, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }

            var pagedData = PagedUtil.Pagination<MessageResponse>(validPaged, messages);

            validPaged.PageSize = (totalMessage < validPaged.PageSize) ? totalMessage : validPaged.PageSize;

            var pagedReponse = PaginationHelper.CreatePagedReponse<MessageResponse>(pagedData, validPaged, totalMessage, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";

            return pagedReponse;
        }
    }
}
