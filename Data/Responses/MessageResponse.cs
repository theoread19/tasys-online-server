using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class MessageResponse : Response
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        // <summary>
        ///     Property for content of message
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        ///     Property for file url of message
        /// </summary>
        public string? FileURL { get; set; }

        /// <summary>
        ///     Property for sender id
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        ///     Property for recipient id
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        ///     Property for course table is recipient
        ///     one to many
        /// </summary>
        public CourseResponse Course { get; set; }
    }
}
