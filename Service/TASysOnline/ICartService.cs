using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface ICartService
    {
        public Task<IEnumerable<CartResponse>> GetAllCartAsync();

        public Task<PageResponse<List<CartResponse>>> GetAllCartPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateCartAsync(CartRequest cartRequest);

        public Task<CartResponse> GetCartByUserId(Guid userId, AccountAuthorInfo accountAuthorInfo);

        public Task<Response> UpdateCart(CartRequest cartRequest, AccountAuthorInfo accountAuthorInfo);

        public Task<Response> DeleteCart(Guid[] cartId);

        public Task<FilterResponse<List<CartResponse>>> FilterCartBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<CartResponse>>> SearchCartBy(Search searchRequest, string route);

        public Task<Response> DeleteAllCart();

        public Task<Response> AddCourseToCart(Guid userId, Guid courseId);

        public Task<Response> RemoveCourseFromCart(Guid userId, Guid courseId);

        public Task<Response> RemoveAllCourseFromCart(Guid userId);
    }
}
