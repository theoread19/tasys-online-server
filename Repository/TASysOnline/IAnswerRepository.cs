using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface IAnswerRepository : IRepository<AnswerTable>
    {
        public Task<List<AnswerTable>> FindByQuestionId(Guid questionId);
    }
}
