using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class LessonRepository : BaseRepository<LessonTable>, ILessonRepository
    {
        private TASysOnlineContext _context;

        public LessonRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }
    }
}
