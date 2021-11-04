using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface IBillRepository : IRepository<BillTable>
    {

        public Task<BillTable> GetByIdEagerLoad(Guid Id);

        public Task AddCourseToBill(Guid billId, CourseTable courseTable);
    }
}
