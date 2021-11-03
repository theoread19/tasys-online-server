using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;

namespace TASysOnlineProject.Utils
{
    public class FilterSearchUtil
    {
        public static List<T> FilterSearch<T>(FilterSearch filterSearch, List<T> data)
        {
            try
            {
                var sortBy = filterSearch.SortBy;
                var propertySort = typeof(T).GetProperty(sortBy!);

                if (propertySort == null)
                {
                    propertySort = typeof(T).GetProperty("CreatedDate");
                }

                var searchBy = filterSearch.SearchProperty;
                var propertySearch = typeof(T).GetProperty(searchBy!);

                var unSignSearchValue = ConvertUtil.ConvertToUnSign(filterSearch.SearchValue!);

                var filterBy = filterSearch.FilterProperty;
                var propertyfilter = typeof(T).GetProperty(filterBy!);

                var filterData = data
                    .Where(s => propertyfilter!.GetValue(s, null)!.ToString() == filterSearch.FilterValue).ToList();

                var searchData = (filterSearch.SearchValue == string.Empty) ? data
                    : filterData.Where(w =>
                {
                    if (ConvertUtil.ConvertToUnSign(propertySearch!.GetValue(w, null)!.ToString()!).IndexOf(unSignSearchValue, StringComparison.CurrentCultureIgnoreCase) >= 0)
                        return true;
                    else
                        return false;
                }).ToList();

                var sortData = filterSearch.Order!.Equals("asc") ?
                    searchData.OrderBy(x => propertySort!.GetValue(x, null))
                    : searchData.OrderByDescending(x => propertySort!.GetValue(x, null));

                var pagedData = sortData
                    .Skip((filterSearch.PageNumber - 1) * filterSearch.PageSize)
                    .Take(filterSearch.PageSize)
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
