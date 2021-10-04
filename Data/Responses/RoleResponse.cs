using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class RoleResponse : Response
    {
        /// <summary>
        ///     Property for id of role respone
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Property for name of role respone
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     Property for created date of role respone
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
