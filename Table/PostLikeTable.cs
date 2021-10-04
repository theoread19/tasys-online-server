using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for post like
    /// </summary>
    public class PostLikeTable : BaseTable
    {
        /// <summary>
        ///     Property for post id
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        ///     Property for post table
        ///     one to many
        /// </summary>
        public PostTable? Post { get; set; }

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
