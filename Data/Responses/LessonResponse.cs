using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class LessonResponse : Response
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        ///     Property for name of lesson
        /// </summary>
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

        public bool IsFront { get; set; } = true;

    }
}
