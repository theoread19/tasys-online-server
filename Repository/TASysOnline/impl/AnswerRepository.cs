using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class AnswerRepository : BaseRepository<AnswerTable>, IAnswerRepository
    {
        public TASysOnlineContext _context;

        public AnswerRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<List<AnswerTable>> FindByQuestionId(Guid questionId)
        {
            return await this._context.AnswerTables.Where(w => w.QuestionId.Equals(questionId)).ToListAsync();
        }
    }
}

