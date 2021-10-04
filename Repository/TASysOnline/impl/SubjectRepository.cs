using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class SubjectRepository : BaseRepository<SubjectTable>, ISubjectRepository
    {
        public TASysOnlineContext _context { get; set; }

        public SubjectRepository() : base (new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<SubjectTable> FindByNameAsync(string name)
        {
            return await this._context.SubjectTables.Where(w => w.Name!.Equals(name)).FirstOrDefaultAsync();
        }

    }
}
