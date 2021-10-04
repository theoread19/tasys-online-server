using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class UserAccountAuthRequest
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }
    }
}
