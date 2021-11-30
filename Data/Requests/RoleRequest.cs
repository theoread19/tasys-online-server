using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class RoleRequest
    {
        /// <summary>
        ///     Property for name of role
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        ///     Property for created date
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
