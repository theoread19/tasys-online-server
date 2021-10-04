using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for app config
    /// </summary>
    public class AppConfigTable : BaseTable
    {
        /// <summary>
        ///     Property for property name of app config
        /// </summary>
        public string? PropertyName { get; set; }

        /// <summary>
        ///     Property for theme table
        ///     one to one
        /// </summary>
        public ThemeTable? Theme { get; set; }

        /// <summary>
        ///     Property for localization table
        ///     one to one
        /// </summary>
        public LocalizationTable? Localization { get; set; }
    }
}
