using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for user ranking
    /// </summary>
    public class UserRankingTable : BaseTable
    {
        /// <summary>
        ///     Property for user ranking
        /// </summary>
        public int TotalScore { get; set; }

        /// <summary>
        ///     Property for user id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        ///     Property for user table
        ///     one to many
        /// </summary>
        public UserAccountTable? UserAccount { get; set; }

        /// <summary>
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        ///     Property for course table
        ///     one to many
        /// </summary>
        public CourseTable? Course { get; set; }
    }
}
