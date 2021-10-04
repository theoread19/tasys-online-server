using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for role
    /// </summary>
    public class RoleTable : BaseTable
    {
        /// <summary>
        ///     Property for name of role
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     List for user account table
        ///     many to one
        /// </summary>
        public ICollection<UserAccountTable>? UserAccounts { get; set; }
    }
}
