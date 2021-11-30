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
        public Task<Response> CreateCartAsync(CartRequest cartRequest);

        public Task<CartResponse> GetCartByUserId(Guid userId, AccountAuthorInfo accountAuthorInfo);

        public Task<Response> AddCourseToCart(Guid userId, Guid courseId);

        public Task<Response> RemoveCourseFromCart(Guid userId, Guid courseId);

        public Task<Response> RemoveAllCourseFromCart(Guid userId, bool isCreateBill = false);
    }
}
