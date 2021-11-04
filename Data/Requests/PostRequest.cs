using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class PostRequest
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     Property for title post
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        ///     Property for content of post
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }

        /// <summary>
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }
    }
}
