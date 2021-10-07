using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;

namespace TASysOnlineProject.Utils
{
    public class PagedUtil
    {
        public static List<T> Pagination<T>(Pagination pagination, List<T> data)
        {
            try
            {
                var sortBy = pagination.SortBy;
                var propertySort = typeof(T).GetProperty(sortBy!);

                if (propertySort == null)
                {
                    propertySort = typeof(T).GetProperty("CreatedDate");
                }

                var sortData = pagination.Order!.Equals("asc") ?
                data.OrderBy(x => propertySort!.GetValue(x, null))
                : data.OrderByDescending(x => propertySort!.GetValue(x, null));

                var pagedData = sortData
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                    .Take(pagination.PageSize)
                    .ToList();

                return pagedData;
            }
            catch
            {
                return new List<T>();
            }

        }
    }
}
