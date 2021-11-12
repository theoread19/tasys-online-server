using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class QuestionRepository : BaseRepository<QuestionTable>, IQuestionRepository
    {
        public TASysOnlineContext _context;

        public QuestionRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<QuestionTable> FindQuestionByIdEagerAsync(Guid Id)
        {
            try
            {
                var table = await this._context.QuestionTables
                .Where(w => w.Id == Id)
                .Include(i => i.Answers)
                .FirstOrDefaultAsync();

                return table;
            }
            catch
            {
                return null;
            }

        }

        public async Task<List<QuestionTable>> FindQuestionByTestIdEagerAsync(Guid testId)
        {
            try
            {
                var table = await this._context.QuestionTables
                .Where(w => w.TestId == testId)
                .Include(i => i.Answers)
                .ToListAsync();
                return table;
            }
            catch
            {
                return new List<QuestionTable>();
            }

        }
    }
}
