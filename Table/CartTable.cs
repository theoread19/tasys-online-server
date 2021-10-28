using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for cart
    /// </summary>
    public class CartTable : BaseTable
    {
        /// <summary>
        ///     Property for total scoure of cart
        /// </summary>
        public int TotalCourse { get; set; }

        /// <summary>
        ///     Property for total cost of cart
        /// </summary>
        public decimal TotalCost { get; set; }

        public CartTable()
        {
            this.Courses = new HashSet<CourseTable>();
        }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }

        /// <summary>
        ///     Property for user account
        ///     one to one
        /// </summary>
        public UserAccountTable? UserAccount { get; set; }

        /// <summary>
        ///     List of couser table
        ///     many to many
        /// </summary>
        public virtual ICollection<CourseTable> Courses { get; set; }
    }
}
