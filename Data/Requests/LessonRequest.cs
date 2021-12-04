using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class LessonRequest
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     Property for name of lesson
        /// </summary>
        [Required]
        [MinLength(1, ErrorMessage = "Content must be min 1 character.")]
        public string? Name { get; set; }

        /// <summary>
        ///     Property for description of lesson
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///     Property for backtext of lesson
        /// </summary>
        public string? BackText { get; set; }

        /// <summary>
        ///     Property for frontext of lesson
        /// </summary>
        public string? FrontText { get; set; }

        /// <summary>
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }
    }
}
