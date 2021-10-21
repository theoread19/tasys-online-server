using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class AccountAuthorInfo
    {
        public Guid? Id { get; set; }

        public string? Role { get; set; }

        public string? Username { get; set; }
    }
}
