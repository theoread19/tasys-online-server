using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class MediaResponse : Response
    {

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        /// <summary>
        ///     Property for source id of media
        /// </summary>
        public Guid? SourceID { get; set; }

        /// <summary>
        ///     Property for type of media
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        ///     Property for subtitle of media
        /// </summary>
        public string? Container { get; set; }

        /// <summary>
        ///      Property for title of media
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        ///      Property for name of media
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        ///      Property for file size of media
        /// </summary>
        public ulong FileSize { get; set; }

        /// <summary>
        ///      Property for file type of media
        /// </summary>
        public string? FileType { get; set; }

        /// <summary>
        ///     Property for url of media
        /// </summary>
        public string? Url { get; set; }
    }
}
