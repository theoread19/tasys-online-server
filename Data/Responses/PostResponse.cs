using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class PostResponse : Response
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

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

        public int CountPostLike { get; set; }
        public UserAccountResponse UserAccountResponse { get; set; }

        public List<PostLikeResponse>? postLikeResponses { get; set; }
    }
}
