using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class base for all table
    /// </summary>
    public class BaseTable
    {
        /// <summary>
        ///     Property for identity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Property for created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        ///     Property for modified date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
