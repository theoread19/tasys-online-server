using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface ICommentService
    {
        public Task<IEnumerable<CommentResponse>> GetAllCommentAsync();

        public Task<PageResponse<List<CommentResponse>>> GetAllCommentPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateCommentAsync(CommentRequest commentRequest);

        public Task<CommentResponse> GetCommentById(Guid id);

        public Task<Response> UpdateComment(CommentRequest commentRequest);

        public Task<Response> DeleteComment(Guid[] commentId);

        public Task<FilterResponse<List<CommentResponse>>> FilterCommentBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<CommentResponse>>> SearchCommentBy(Search searchRequest, string route);

        public Task<Response> DeleteAllComment();
    }
}
