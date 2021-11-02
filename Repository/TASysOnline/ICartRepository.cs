using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface ICartRepository : IRepository<CartTable>
    {
        public Task<CartTable> GetCartByUserIdAsync(Guid userId);

        public Task AddCourseToCart(CourseTable courseTable, Guid cartId);

        public Task RemoveCourseFromCart(Guid courseId, Guid cartId);

    }
}
