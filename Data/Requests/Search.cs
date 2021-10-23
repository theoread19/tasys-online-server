using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data
{
    public class Search : Pagination
    {
        public string? Value { get; set; }

        public string? Property { get; set; }

        public Search() : base()
        {
            this.Value = "";
            this.Property = "";
        }

        public Search(int pageNumber, int pageSize, string sortBy, string order, string value, string property) : base(pageNumber, pageSize, sortBy, order)
        {
            this.Value = value != null ? this.Value = value : null;
            this.Property = (property == null || property == "") ? this.Property = "" : this.Property = char.ToUpper(property[0]) + property.Substring(1);
        }
    }
}
