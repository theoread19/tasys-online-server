using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for schedule
    /// </summary>
    public class ScheduleTable : BaseTable
    {
        /// <summary>
        ///     Property for start time of schedule
        /// </summary>
        public float StartTime { get; set; }

        /// <summary>
        ///     Property for end time of schedule
        /// </summary>
        public float EndTime { get; set; }

        /// <summary>
        ///     Property for date of week of schedule
        /// </summary>
        public int DayOfWeek { get; set; }

        /// <summary>
        ///     List for course table
        ///     many to one
        /// </summary>
        public List<CourseTable>? Courses { get; set; }
    }
}
