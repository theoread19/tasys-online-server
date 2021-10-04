using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class DiscussionRequest
    {
        /// <summary>
        ///     Property for sender id
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        ///     Property for recipient id
        /// </summary>
        public Guid RecipientId { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public DiscussionRequest()
        {
            this.PageNumber = 1;
            this.PageSize = 100;
        }
        public DiscussionRequest(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = ((pageSize > 100) || (pageSize < 0)) ? 100 : pageSize;
        }
    }
}
