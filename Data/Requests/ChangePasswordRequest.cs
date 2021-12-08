using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class ChangePasswordRequest
    {
        [Required]
        [MinLength(6, ErrorMessage = "Password must be min 6 character and max 16 character.")]
        [MaxLength(16, ErrorMessage = "Password must be min 6 character and max 16 character.")]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be min 6 character and max 16 character.")]
        [MaxLength(16, ErrorMessage = "Password must be min 6 character and max 16 character.")]
        public string NewPassword { get; set; }
    }
}
