using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class MediasRequest
    {
        public Guid[] Id { get; set; }

        [Required]
        public string targetContainer { get; set; }
    }
}
