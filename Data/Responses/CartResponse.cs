using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class CartResponse : Response
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        /// <summary>
        ///     Property for total scoure of cart
        /// </summary>
        public int TotalCourse { get; set; }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }
    }
}
