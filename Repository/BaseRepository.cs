using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TASysOnlineProject.Data;

namespace TASysOnlineProject.Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository(DbContext context)
        {
            this._context = context;
            _dbSet = this._context.Set<TEntity>();

        }

        public async Task<int> CountAsync()
        {
            return await this._dbSet.CountAsync();
        }

        public async Task<int> CountByAsync(string property, string value)
        {
            var filterBy = property;

            var propertyfilter = typeof(TEntity).GetProperty(filterBy!);

            if (propertyfilter == null)
            {
                return 0;
            }

            var data = await this._dbSet.ToListAsync();
            var unSignValue = ConvertToUnSign(value);

            var countData = data.Where(w =>
            {
                if (ConvertToUnSign(propertyfilter!.GetValue(w, null)!.ToString()!).IndexOf(unSignValue, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    return true;
                else
                    return false;
            }).AsQueryable().Count();

            return countData;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var std = await this._dbSet.FindAsync(id);
            this._dbSet.Remove(std);

        }

        public async Task<IEnumerable<TEntity>> GetAllWithEagerLoad(Expression<Func<TEntity, bool>> filter, string[] children)
        {
            IQueryable<TEntity> query = this._dbSet;
            foreach (string entity in children)
            {
                query = query.Include(entity);

            }
            return await query.Where(filter).ToListAsync();
        }

        public async Task<List<TEntity>> SearchByAsync(Search search)
        {
            var sortBy = search.SortBy;
            var propertySort = typeof(TEntity).GetProperty(sortBy!);

            if (propertySort == null)
            {
                propertySort = typeof(TEntity).GetProperty("CreatedDate");
            }

            var filterBy = search.Property;
            var propertyfilter = typeof(TEntity).GetProperty(filterBy!);

            var data = await this._dbSet.ToListAsync();

            var unSignValue = ConvertToUnSign(search.Value!);

            var searchData = data.Where(w =>
                {
                    if (ConvertToUnSign(propertyfilter!.GetValue(w, null)!.ToString()!).IndexOf(unSignValue, StringComparison.CurrentCultureIgnoreCase) >= 0)
                        return true;
                    else
                        return false;
                });

            var sortData = search.Order!.Equals("asc") ?
                searchData.OrderBy(x => propertySort!.GetValue(x, null))
                : searchData.OrderByDescending(x => propertySort!.GetValue(x, null));

            var pagedData = sortData
                .Skip((search.PageNumber - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToList();

            return pagedData;
        }


        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            List<TEntity> models = await this._dbSet.ToListAsync();
            return models;
        }

        public async Task<List<TEntity>> GetAllPadingAsync(Pagination pagination)
        {   
            var param = pagination.SortBy;
            var propertyInfo = typeof(TEntity).GetProperty(param!);

            if (propertyInfo == null)
            {
                propertyInfo = typeof(TEntity).GetProperty("CreatedDate");
            }

            var data = await this._dbSet.ToListAsync();
            var sortData = pagination.Order!.Equals("asc") ?
                data.OrderBy(x => propertyInfo!.GetValue(x, null))
                : data.OrderByDescending(x => propertyInfo!.GetValue(x, null));

            var pagedData = sortData
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();
            return pagedData;
        }

        public virtual async Task<TEntity> FindByIdAsync(Guid id)
        {
            var std = await this._dbSet.FindAsync(id);
            return std;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity table)
        {
            var result = await this._dbSet.AddAsync(table);
            return result.Entity;
        }

        public virtual async Task SaveAsync()
        {
            await this._context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity table)
        {
            this._context.Entry(table).State = EntityState.Modified;
        }

        public async Task<List<TEntity>> FilterByAsync(Filter filter)
        {
            var sortBy = filter.SortBy;
            var propertySort = typeof(TEntity).GetProperty(sortBy!);

            if(propertySort == null)
            {
                propertySort = typeof(TEntity).GetProperty("CreatedDate");
            }

            var filterBy = filter.Property;
            var propertyfilter = typeof(TEntity).GetProperty(filterBy!);

            var data = await this._dbSet.ToListAsync();

            var filterData = data
                .Where(s => propertyfilter!.GetValue(s, null)!.ToString()! == filter.Value);

            var sortData = filter.Order!.Equals("asc") ?
                filterData.OrderBy(x => propertySort!.GetValue(x, null))
                : filterData.OrderByDescending(x => propertySort!.GetValue(x, null));

            var pagedData = sortData
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            return pagedData;
        }

        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
/*            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }*/
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

        public async Task DeleteAllAsyn()
        {
            foreach (var entities in this._dbSet)
            {
                this._dbSet.Remove(entities);
            }
        }
    }
}
