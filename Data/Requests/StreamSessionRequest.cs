using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class StreamSessionRequest
    {
        public Guid Id { get; set; }

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
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }
    }
}
