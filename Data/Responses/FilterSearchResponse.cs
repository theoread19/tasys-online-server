using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class FilterSearchResponse<T> : PageResponse<T>
    {
        public string? FilterValue { get; set; }
        public string? SearchValue { get; set; }
        public string? FilterProperty { get; set; }
        public string? SearchProperty { get; set; }
        public FilterSearchResponse(T data,
                        int pageNumber,
                        int pageSize,
                        string filterValue,
                        string filterProperty,
                        string searchValue,
                        string searchProperty) : base(data, pageNumber, pageSize)
        {
            this.FilterValue = filterValue;
            this.FilterProperty = filterProperty;

            this.SearchValue = searchValue;
            this.SearchProperty = searchProperty;
        }
    }
}
