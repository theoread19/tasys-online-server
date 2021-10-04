using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class LoginRequest
    {
        /// <summary>
        ///     Property for username of authenticate request
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        ///     Property for password of authenticate request
        /// </summary>
        public string? Password { get; set; }
    }
}
