using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class MediaRepository : BaseRepository<MediaTable>, IMediaRepository
    {
        public TASysOnlineContext _context;

        public MediaRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        public async Task<List<MediaTable>> FindByContainerNameAsync(string containerName)
        {
            var table = await this._context.MediaTables.Where(w => w.Container.Equals(containerName)).ToListAsync();
            return table;
        }
    }
}
