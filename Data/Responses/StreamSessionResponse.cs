using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Data.Responses
{
    public class StreamSessionResponse : Response
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

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
        public UserAccountResponse? Creator { get; set; }

        /// <summary>
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        ///     Property for course table
        ///     one to many
        /// </summary>
        public CourseResponse? CourseTable { get; set; }
    }
}
