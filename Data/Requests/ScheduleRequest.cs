using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Data.Requests
{
    /// <summary>
    ///  Wait for done paypal
    /// </summary>
    public class ScheduleRequest
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
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }
    }
}
