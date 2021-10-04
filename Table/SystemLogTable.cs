using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for system log
    /// </summary>
    public class SystemLogTable : BaseTable 
    {
        /// <summary>
        ///     Property for message of system log
        /// </summary>
        public string? Message { get; set; }

    }
}
