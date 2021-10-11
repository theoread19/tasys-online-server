using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface IStreamSessionRepository : IRepository<StreamSessionTable>
    {
        public Task<List<StreamSessionTable>> GetAllStreamSessionEagerLoadAsync();

        public Task<List<StreamSessionTable>> GetComingStreamSessionEagerLoadAsync(DateTime now);
    }
}
