using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class CourseRequest
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     Property for name of course
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     Property for summary of course
        /// </summary>
        [Required]
        public string Summary { get; set; }

        /// <summary>
        ///     Property for duration of course (unit in minute)
        /// </summary>
        [Required]
        public int Duration { get; set; }

        /// <summary>
        ///     Property for description of course
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        ///     Property for available slot of course
        /// </summary>
        [Required]
        public int AvailableSlot { get; set; }

        /// <summary>
        ///     Property for max slot of course
        /// </summary>
        [Required]
        public int MaxSlot { get; set; }

        /// <summary>
        ///     Property for cost of course
        /// </summary>
        [Required]
        public decimal Cost { get; set; }

        /// <summary>
        ///     Property for subject id
        /// </summary>
        [Required]
        public Guid SubjectId { get; set; }

        /// <summary>
        ///     Property for user id is instructor
        /// </summary>
        [Required]
        public Guid InstructorId { get; set; }
    }
}
