using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class ContainerResponse : Response
    {
        public string Name { get; set; }

        public BlobContainerProperties Properties { get; set; }
    }
}
