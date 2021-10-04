using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for curriculum 
    /// </summary>
    public class CurriCulumTable : BaseTable
    {
        /// <summary>
        ///     Property for name of curriculum
        /// </summary>
        public string? Name { get; set; }

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
