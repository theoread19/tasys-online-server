using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class BillResponse : Response
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
        /// <summary>
        ///     Property for description of bill
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///     Property for total cost of bill
        /// </summary>
        public decimal TotalCost { get; set; }

        /// <summary>
        ///     Property for total item of bill
        /// </summary>
        public int TotalItem { get; set; }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }

        public List<CourseResponse>? Courses { get; set; }

    }
}
