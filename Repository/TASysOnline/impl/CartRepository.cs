using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class CartRepository : BaseRepository<CartTable>, ICartRepository
    {
        public TASysOnlineContext _context;

        public CartRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task AddCourseToCart(CourseTable courseTable, Guid cartId)
        {
            var table = await this._context.CartTables.Where(w => w.Id == cartId).FirstOrDefaultAsync();
            table.Courses.Add(courseTable);
            await this._context.SaveChangesAsync();
        }

        public async Task<CartTable> GetCartByUserIdAsync(Guid userId)
        {
            try
            {
                var table = await this._context.CartTables.Where(w => w.UserAccountId == userId)
                                                            .Include(i => i.Courses)
                                                            .FirstOrDefaultAsync();
                return table;
            }
            catch
            {
                return null;
            }
        }

        public async Task RemoveCourseFromCart(Guid courseId, Guid cartId)
        {
            var table = await this._context.CartTables.Where(w => w.Id == cartId).FirstOrDefaultAsync();
            var course = table.Courses.Where(w => w.Id == courseId).FirstOrDefault();
            table.Courses.Remove(course);
            await this._context.SaveChangesAsync();
        }
    }
}
