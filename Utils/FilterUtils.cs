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
                var filterBy = filter.Property;
                var propertyfilter = typeof(T).GetProperty(filterBy!);

                var filterData = data
                    .Where(s => propertyfilter!.GetValue(s, null)!.ToString()! == filter.Value)
                    .ToList();

                return filterData;
            }
            catch
            {
                return new List<T>();
            }
            
        }
    }
}
