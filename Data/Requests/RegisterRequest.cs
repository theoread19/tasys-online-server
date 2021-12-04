using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class RegisterRequest
    {
        /// <summary>
        ///     Property for username of authenticate request
        /// </summary>
        [Required]
        [MinLength(6, ErrorMessage = "Username must be min 6 character and max 12 character.")]
        [MaxLength(12, ErrorMessage = "Username must be min 6 character and max 12 character.")]
        public string? Username { get; set; }

        /// <summary>
        ///     Property for password of authenticate request
        /// </summary>
        [Required]
        [MinLength(6, ErrorMessage = "Password must be min 6 character and max 16 character.")]
        [MaxLength(16, ErrorMessage = "Password must be min 6 character and max 16 character.")]
        public string? Password { get; set; }

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
