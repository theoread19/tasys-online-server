using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface IQuestionRepository : IRepository<QuestionTable>
    {
        public Task<QuestionTable> FindQuestionByIdEagerAsync(Guid Id);

        public Task<List<QuestionTable>> FindQuestionByTestIdEagerAsync(Guid testId);
    }
}
