using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TASysOnlineProject.Data;

namespace TASysOnlineProject.Utils
{
    public class FilterUtils
    {

        public static List<T> Filter<T>(Filter filter, List<T> data)
        {
            try
            {
                var sortBy = filter.SortBy;
                var propertySort = typeof(T).GetProperty(sortBy!);

                if (propertySort == null)
                {
                    propertySort = typeof(T).GetProperty("CreatedDate");
                }

                var filterBy = filter.Property;
                var propertyfilter = typeof(T).GetProperty(filterBy!);

                var filterData = data
                    .Where(s => propertyfilter!.GetValue(s, null)!.ToString()! == filter.Value)
                    .ToList();

                var sortData = filter.Order!.Equals("asc") ?
                    filterData.OrderBy(x => propertySort!.GetValue(x, null))
                    : filterData.OrderByDescending(x => propertySort!.GetValue(x, null));

                var pagedData = sortData
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
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
