using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for post
    /// </summary>
    public class PostTable : BaseTable
    {
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
        ///     Property for user account table
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
        public CourseTable? CourseTable { get; set; }

        /// <summary>
        ///     List of comment table
        ///     many to one
        /// </summary>
        public ICollection<CommentTable>? Comments { get; set; }

        /// <summary>
        ///     List of post like table
        ///     many to one
        /// </summary>
        public ICollection<PostLikeTable>? PostLikes { get; set; }
    }
}
