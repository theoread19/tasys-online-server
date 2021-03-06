using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class FilterResponse<T> : PageResponse<T>
    {
        public string? Value { get; set; }

        public string? Property { get; set; }

        public FilterResponse(T data, int pageNumber, int pageSize, string value, string property) : base(data, pageNumber, pageSize)
        {
            this.Value = value;
            this.Property = property;
        }
    }
}
