using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class MediasRequest
    {
        public Guid[] Id { get; set; }

        public string targetContainer { get; set; }
    }
}
