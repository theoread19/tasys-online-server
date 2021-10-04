using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;

namespace TASysOnlineProject.Data.Requests
{
    public class SearchRequest : Pagination
    {
        public string? Value { get; set; }

        public string? Property { get; set; }

        public SearchRequest(int pageNumber, int pageSize, string sortBy, string order, string searchString, string property) : base(pageNumber, pageSize, sortBy, order)
        {
            this.Value = searchString != null ? this.Value = searchString : null;
            this.Property = property;
        }
    }
}
