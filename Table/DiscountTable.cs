using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for discount
    /// </summary>
    public class DiscountTable : BaseTable
    {
        /// <summary>
        ///     Property for title of discount
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        ///     Property for rate of discount
        /// </summary>
        public float Rate { get; set; }

        /// <summary>
        ///     Property for duration of discount
        ///     month
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        ///     Property for course table,
        ///     one to one
        /// </summary>
        public CourseTable? Course { get; set; }
    }
}
