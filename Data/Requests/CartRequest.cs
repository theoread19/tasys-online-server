using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class CartRequest
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     Property for total scoure of cart
        /// </summary>
        public int TotalCourse { get; set; }

        /// <summary>
        ///     Property for total cost of cart
        /// </summary>
        public decimal TotalCost { get; set; }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }

        public CartRequest Create(Guid userId)
        {
            return new CartRequest {
                UserAccountId = userId,
                TotalCourse = 0
            };            
        }
    }
}
