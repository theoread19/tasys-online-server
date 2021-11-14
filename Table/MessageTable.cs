using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for message
    /// </summary>
    public class MessageTable : BaseTable
    {
        /// <summary>
        ///     Property for content of message
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        ///     Property for sender id
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        ///     Property for user account table is sender
        ///     one to many
        /// </summary>
        public UserAccountTable? Sender { get; set; }

        /// <summary>
        ///     Property for recipient id
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        ///     Property for course table is recipient
        ///     one to many
        /// </summary>
        public CourseTable Course { get; set; }
    }
}
