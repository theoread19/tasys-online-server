using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IPostService
    {
        public Task<IEnumerable<PostResponse>> GetAllPostAsync();

        public Task<PageResponse<List<PostResponse>>> GetAllPostPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreatePostAsync(PostRequest postRequest);

        public Task<PostResponse> GetPostById(Guid id);

        public Task<Response> UpdatePost(PostRequest postRequest);

        public Task<Response> DeletePost(Guid[] PostId);

        public Task<FilterResponse<List<PostResponse>>> FilterPostBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<PostResponse>>> SearchPostBy(Search searchRequest, string route);

        public Task<FilterSearchResponse<List<PostResponse>>> FilterSearchPostBy(FilterSearch filterSearchRequest, string route);

        public Task<Response> DeleteAllPost();
    }
}
