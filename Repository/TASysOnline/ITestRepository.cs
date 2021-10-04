using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface ITestRepository : IRepository<TestTable>
    {
        public Task<TestTable> FindTestByIdEagerAsync(Guid id);
    }
}
