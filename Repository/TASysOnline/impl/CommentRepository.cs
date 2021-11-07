using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class CommentRepository : BaseRepository<CommentTable>, ICommentRepository
    {
        public TASysOnlineContext _context;

        public CommentRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<List<CommentTable>> GetAllCommentTablesEagerLoad()
        {
            var tables = await this._context.CommentTables
                                            .Include(i => i.UserAccount)
                                            .ToListAsync();

            return tables;
        }
    }
}
