using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class DiscountRequest 
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     Property for title of discount
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        ///     Property for rate of discount
        /// </summary>
        public float Rate { get; set; }

        /// <summary>
        ///     Property for duration of discount
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }
    }
}
