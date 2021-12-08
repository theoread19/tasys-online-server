using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class UserAccountUpdateRequest
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     Property for username of authenticate request
        /// </summary>
        public string? Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "DisplayName must be min 6 character and max 20 character.")]
        [MaxLength(20, ErrorMessage = "DisplayName must be min 6 character and max 20 character.")]
        public string? DisplayName { get; set; }

        /// <summary>
        ///     Property for role id of authenticate request
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
