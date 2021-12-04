using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [MinLength(1, ErrorMessage = "Content must be min 1 character.")]
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
