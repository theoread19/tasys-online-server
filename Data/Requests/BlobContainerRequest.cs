using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class BlobContainerRequest
    {
        public string? FileName { get; set; }

        public string? FileContain { get; set; }

        public string? FileType { get; set; }

        public string? FileDirectory { get; set; }
    }
}
