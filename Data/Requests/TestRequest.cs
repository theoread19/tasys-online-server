using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class TestRequest
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     Property for name of test
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        ///     Property for description of test
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///     Property for allocated time of test
        /// </summary>
        [Range(1, 99999)]
        public int AllocatedTime { get; set; }

        /// <summary>
        ///     Property for deadline of test
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        ///     Property for max questions of test
        /// </summary>
        [Range(1, 99999)]
        public int MaxQuestion { get; set; }

        /// <summary>
        ///     Property for max score of test
        /// </summary>
        [Range(1, 99999)]
        public int MaxScore { get; set; }

        /// <summary>
        ///     Property for max attempt for test
        /// </summary>
        [Range(1, 99999)]
        public int MaxAttempt { get; set; }

        /// <summary>
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }

    }
}
