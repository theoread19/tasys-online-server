using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class MediaChangeNameRequest
    {
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        ///      Property for name of media
        /// </summary>
        [Required]
        public string? FileName { get; set; }
    }
}
