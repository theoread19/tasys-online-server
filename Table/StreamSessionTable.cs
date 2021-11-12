using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for stream session
    /// </summary>
    public class StreamSessionTable : BaseTable
    {
        /// <summary>
        ///     Property fot start time of stream session
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     Property for end time of stream session
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        ///     Property for creator id
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        ///     Property for user account table is creator
        ///     one to many
        /// </summary>
        public UserAccountTable? Creator { get; set; }

        /// <summary>
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        ///     Property for course table
        ///     one to many
        /// </summary>
        public CourseTable? CourseTable { get; set; }
    }
}
