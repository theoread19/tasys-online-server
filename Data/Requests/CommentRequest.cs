using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class CommentRequest
    {

        public Guid Id { get; set; }
        /// <summary>
        ///     Property for content of comment
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        ///     Property for post id
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }
    }
}
