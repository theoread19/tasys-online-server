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
        [MinLength(1, ErrorMessage = "Name must be min 1 character.")]
        public string? Name { get; set; }

        /// <summary>
        ///     Property for summary of course
        /// </summary>
        [Required]
        [MinLength(1, ErrorMessage = "Summary must be min 1 character.")]
        public string? Summary { get; set; }

        /// <summary>
        ///     Property for duration of course (unit in minute)
        /// </summary>
        [Range(1,12)]
        public int Duration { get; set; }

        /// <summary>
        ///     Property for description of course
        /// </summary>
        [Required]
        [MinLength(1, ErrorMessage = "Description must be min 1 character.")]
        public string? Description { get; set; }

        /// <summary>
        ///     Property for available slot of course
        /// </summary>
        [Range(1, 99999)]
        public int AvailableSlot { get; set; }

        /// <summary>
        ///     Property for max slot of course
        /// </summary>
        [Range(1, 99999)]
        public int MaxSlot { get; set; }

        /// <summary>
        ///     Property for cost of course
        /// </summary>
        [Range(1, 99999)]
        public decimal Cost { get; set; }

        /// <summary>
        ///     Property for subject id
        /// </summary>
        public Guid SubjectId { get; set; }

        /// <summary>
        ///     Property for user id is instructor
        /// </summary>
        public Guid InstructorId { get; set; }
    }
}
