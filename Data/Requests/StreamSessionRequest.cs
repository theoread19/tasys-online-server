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
        public string StartTime { get; set; }

        /// <summary>
        ///     Property for end time of stream session
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        ///     Property for max paticipant of session
        /// </summary>
        public int MaxParticipants { get; set; }

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
