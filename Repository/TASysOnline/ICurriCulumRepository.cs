using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface ICurriCulumRepository : IRepository<CurriCulumTable>
    {
        public Task<List<CurriCulumTable>> GetAllCurriCulumTablesEagerLoad();

        public Task<CurriCulumTable> FindByIdEagerLoad(Guid id);
    }
}
