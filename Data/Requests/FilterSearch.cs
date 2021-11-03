using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class FilterSearch : Pagination
    {
        public string? FilterValue { get; set; }
        public string? SearchValue { get; set; }
        public string? FilterProperty { get; set; }
        public string? SearchProperty { get; set; }

        public FilterSearch() : base()
        {
            this.FilterProperty = "";
            this.SearchProperty = "";
            this.FilterValue = "";
            this.SearchProperty = "";
        }

        public FilterSearch(int pageNumber,
                        int pageSize,
                        string sortBy,
                        string order,
                        string filterValue,
                        string filterProperty,
                        string searchValue,
                        string searchProperty): base(pageNumber, pageSize, sortBy, order)
        {
            this.FilterValue = filterValue ?? "";
            this.FilterProperty = (filterProperty == null || filterProperty == string.Empty) ? this.FilterProperty = "" : char.ToUpper(filterProperty[0]) + filterProperty.Substring(1);

            this.SearchValue = searchValue ?? "";
            this.SearchProperty = (searchProperty == null || searchProperty == string.Empty) ? this.SearchProperty = "" : char.ToUpper(searchProperty[0]) + searchProperty.Substring(1);
        }
            
    }
}
