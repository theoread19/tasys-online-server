using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TASysOnlineProject.Data;

namespace TASysOnlineProject.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public Task<List<TEntity>> GetAllAsync();
        public Task<List<TEntity>> GetAllPadingAsync(Pagination pagination);
        public Task<IEnumerable<TEntity>> GetAllWithEagerLoad(Expression<Func<TEntity, bool>> filter, string[] children);
        public Task<TEntity> FindByIdAsync(Guid id);
        public Task<TEntity> InsertAsync(TEntity table);
        public Task UpdateAsync(TEntity table);
        public Task DeleteAsync(Guid id);
        public Task SaveAsync();
        public Task<int> CountAsync();
        public Task<int> CountByAsync(string property, string value);
        public Task<List<TEntity>> SearchByAsync(Search search);
        public Task<List<TEntity>> FilterByAsync(Filter filter);
        public Task DeleteAllAsyn();
    }
}
