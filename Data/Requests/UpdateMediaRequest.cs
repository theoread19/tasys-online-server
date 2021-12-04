using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class UpdateMediaRequest
    {

        public Guid Id { get; set; }

        /// <summary>
        ///     Property for source id of media
        /// </summary>
        [Required]
        public Guid? SourceID { get; set; }

        /// <summary>
        ///      Property for title of media
        /// </summary>
        [Required]
        public string? Title { get; set; }
    }
}
