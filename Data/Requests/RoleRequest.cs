using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class RoleRequest
    {
        /// <summary>
        ///     Property for name of role
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     Property for created date
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
