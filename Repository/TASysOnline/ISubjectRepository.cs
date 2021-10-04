using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface ISubjectRepository : IRepository<SubjectTable>
    {
        public Task<SubjectTable> FindByNameAsync(string name);
    }
}
