using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class ContainerUpdateRequest
    {
        public string? oldContainerName { get; set; }

        public string? newContainerName { get; set; }
    }
}
