using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class StreamSessionRepository : BaseRepository<StreamSessionTable>, IStreamSessionRepository
    {
        private TASysOnlineContext _context;
        public StreamSessionRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<List<StreamSessionTable>> GetAllStreamSessionEagerLoadAsync()
        {
            var tables = await this._context.StreamSessionTables
                                        .Include(i => i.CourseTable)
                                        .Include(i => i.Creator)
                                        .ToListAsync();
            return tables;
        }

        public async Task<List<StreamSessionTable>> GetComingStreamSessionEagerLoadAsync(DateTime now)
        {
            var tables = await this._context.StreamSessionTables
                                        .Where(w => w.StartTime >= now)
                                        .Include(i => i.CourseTable)
                                        .Include(i => i.Creator)
                                        .ToListAsync();
            return tables;
        }
    }
}
