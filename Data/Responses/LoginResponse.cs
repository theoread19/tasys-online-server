using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class LoginResponse : Response
    {

        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Username { get; set; }

    }
}
