using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for bill
    /// </summary>
    public class BillTable : BaseTable
    {
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

        public BillTable()
        {
            this.CourseTables = new HashSet<CourseTable>();
        }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }

        /// <summary>
        ///     Property for user account table
        ///     one to many
        /// </summary>
        public UserAccountTable? UserAccount { get; set; }

        /// <summary>
        ///     List for courser table
        ///     many to many
        /// </summary>
        public virtual ICollection<CourseTable> CourseTables { get; set; }
    }
}
