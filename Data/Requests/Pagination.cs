using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data
{
    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public string? Order { get; set; }
        public Pagination()
        {
            this.PageNumber = 1;
            this.PageSize = 100;
            this.SortBy = "CreatedDate";
            this.Order = "desc";
        }
        public Pagination(int pageNumber, int pageSize, string sortBy, string order)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = ((pageSize > 100) || (pageSize < 0)) ? 100 : pageSize;
            this.SortBy = (sortBy == null || sortBy == string.Empty) ? this.SortBy = "CreatedDate" : this.SortBy = char.ToUpper(sortBy[0]) + sortBy.Substring(1);
            this.Order = order.Equals("asc") ? this.Order = order : "desc";
        }
    }
}
