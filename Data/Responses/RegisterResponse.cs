using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Data.Responses
{
    public class RegisterResponse : Response
    {
        /// <summary>
        ///     Property for username of authenticate request
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        ///     property for password of authenticate request
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        ///     Property for role id of user
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
