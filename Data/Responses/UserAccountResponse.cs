using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class UserAccountResponse : Response
    {

        public Guid Id { get; set; }
        /// <summary>
        ///     Property for username of authenticate request
        /// </summary>
        public string? Username { get; set; }

        public string? Password { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? DisplayName { get; set; }

        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        ///     Property for role id of user
        /// </summary>
        public Guid RoleId { get; set; }

        public List<CourseResponse>? CourseResponses { get; set; }
    }
}
