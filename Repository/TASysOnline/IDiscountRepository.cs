using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface IDiscountRepository : IRepository<DiscountTable>
    {
        public Task<DiscountTable> FindDiscountTabeByCourseId(Guid courseId);
    }
}
