using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for technical report
    /// </summary>
    public class TechnicalReportTable : BaseTable
    {
        /// <summary>
        ///     Property for reason of technical report
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        ///     Property for comment of technical report
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }

        /// <summary>
        ///     Property for user account table
        ///     one to many
        /// </summary>
        public UserAccountTable? UserAccount { get; set; }
    }
}
