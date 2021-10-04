using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for localization
    /// </summary>
    public class LocalizationTable : BaseTable
    {
        /// <summary>
        ///     Property for system language of localization
        /// </summary>
        public string? SystemLanguage { get; set; }

        /// <summary>
        ///     Property for app config id
        /// </summary>
        public Guid AppConfigId { get; set; }

        /// <summary>
        ///     Property for app config table
        ///     one to one
        /// </summary>
        public AppConfigTable? AppConfig { get; set; }
    }
}
