using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for course suggestion
    /// </summary>
    public class CourseSuggestionTable : BaseTable
    {
        /// <summary>
        ///     Property for suggestion
        /// </summary>
        public string? Suggestion { get; set; }

        /// <summary>
        ///     Property for level importance of suggestion
        /// </summary>
        public int LevelOfImportance { get; set; }

        /// <summary>
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        ///     Property for course table
        ///     one to many
        /// </summary>
        public CourseTable? Course { get; set; }
    }
}
