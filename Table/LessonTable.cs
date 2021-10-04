using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for lesson
    /// </summary>
    public class LessonTable : BaseTable
    {
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

        /// <summary>
        ///     Property for course table
        ///     one to many
        /// </summary>
        public CourseTable? CourseTables { get; set; }
    }
}
