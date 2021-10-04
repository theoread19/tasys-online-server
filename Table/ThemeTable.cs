using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for theme
    /// </summary>
    public class ThemeTable : BaseTable
    {
        /// <summary>
        ///     Property for front family of theme
        /// </summary>
        public string? FrontFamily { get; set; }

        /// <summary>
        ///     Property for secondary accent of theme
        /// </summary>
        public string? SecondaryAccent { get; set; }

        /// <summary>
        ///     Property for device type of theme
        /// </summary>
        public int DeviceType { get; set; }

        /// <summary>
        ///     Property for main accept of theme
        /// </summary>
        public string? MainAccept { get; set; }

        /// <summary>
        ///     Property for fontsize of theme
        /// </summary>
        public int FontSize { get; set; }


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
