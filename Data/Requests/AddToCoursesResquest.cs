using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class AddToCoursesResquest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public List<Guid> CourseIds { get; set; }
    }
}
