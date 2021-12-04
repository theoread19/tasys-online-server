using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class ContainerRequest
    {
        [Required]
        [MinLength(1, ErrorMessage = "Name must be min 1 character.")]
        public string? Name { get; set; }
    }
}
