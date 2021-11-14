using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class MessageRequest
    {

        public Guid Id { get; set; }

        // <summary>
        ///     Property for content of message
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        ///     Property for sender id
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        ///     Property for recipient id
        /// </summary>
        public Guid CourseId { get; set; }
    }
}
