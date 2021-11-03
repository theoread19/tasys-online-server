using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data
{
    public class Filter : Pagination
    {
        public string? Value { get; set; }

        public string? Property { get; set; }

        public Filter() : base()
        {
            this.Value = "";
            this.Property = "";
        }

        public Filter(int pageNumber, int pageSize, string sortBy, string order, string value, string property) : base(pageNumber, pageSize, sortBy, order)
        {
            this.Value = value != null ? this.Value = value : null;
            this.Property = (property == null || property == string.Empty) ? this.Property = "" : this.Property = char.ToUpper(property[0]) + property.Substring(1).Replace("Response", "");
        }
    }
}
