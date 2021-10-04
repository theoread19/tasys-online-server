using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for subject
    /// </summary>
    public class SubjectTable : BaseTable
    {
        /// <summary>
        ///     Property for name of subject
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     List of course table
        ///     many to one
        /// </summary>
        public ICollection<CourseTable>? Courses { get; set; }
    }
}
