using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class ContainerUpdateRequest
    {
        [Required]
        [MinLength(1, ErrorMessage = "OldContainerName must be min 1 character.")]
        public string? OldContainerName { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "NewContainerName must be min 1 character.")]
        public string? NewContainerName { get; set; }
    }
}
