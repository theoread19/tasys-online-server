using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IMessageService
    {
        public Task<IEnumerable<MessageResponse>> GetAllMessageAsync();

        public Task<PageResponse<List<MessageResponse>>> GetAllMessagePagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateMessageAsync(MessageRequest messageRequest);

        public Task<MessageResponse> GetMessageById(Guid id);

        public Task<Response> UpdateMessage(MessageRequest messageRequest);

        public Task<Response> DeleteMessage(Guid[] messageId);

        public Task<FilterResponse<List<MessageResponse>>> FilterMessageBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<MessageResponse>>> SearchMessageBy(Search searchRequest, string route);

        public Task<Response> DeleteAllMessage();
    }
}
