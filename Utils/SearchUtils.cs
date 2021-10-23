using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;

namespace TASysOnlineProject.Utils
{
    public class SearchUtils
    {
        public static List<T> Search<T>(Search search, List<T> data)
        {
            try
            {
                var sortBy = search.SortBy;
                var propertySort = typeof(T).GetProperty(sortBy!);

                if (propertySort == null)
                {
                    propertySort = typeof(T).GetProperty("CreatedDate");
                }

                var searchBy = search.Property;
                var propertyfilter = typeof(T).GetProperty(searchBy!);

                var unSignValue = ConvertUtil.ConvertToUnSign(search.Value!);

                var searchData = (search.Value == string.Empty) ? data
                    : data.Where(w =>
                {
                    if (ConvertUtil.ConvertToUnSign(propertyfilter!.GetValue(w, null)!.ToString()!).IndexOf(unSignValue, StringComparison.CurrentCultureIgnoreCase) >= 0)
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
            catch
            {
                return new List<T>();
            }
        }
    }
}
