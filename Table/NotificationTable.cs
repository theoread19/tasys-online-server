using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for notification
    /// </summary>
    public class NotificationTable : BaseTable
    {
        /// <summary>
        ///     Property for title of notification
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        ///     Property for content of notification
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        ///     Property for is seen of notification
        /// </summary>
        public bool IsSeen { get; set; }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }

        /// <summary>
        ///     Property for user account table
        ///     one to many
        /// </summary>
        public UserAccountTable? UserAccount { get; set; }
    }
}
