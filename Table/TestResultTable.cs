using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for test result
    /// </summary>
    public class TestResultTable : BaseTable
    {
        /// <summary>
        ///     Property for score of test result
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        ///     Property for test id
        /// </summary>
        public Guid TestId { get; set; }

        /// <summary>
        ///     List for test table
        ///     one to many
        /// </summary>
        public virtual TestTable? Test { get; set; }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }

        /// <summary>
        ///     Property for user account
        ///     one to many
        /// </summary>
        public UserAccountTable? UserAccount { get; set; }
    }
}
