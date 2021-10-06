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
        [Required(ErrorMessage = "Name is require")]
        public string? Name { get; set; }

        /// <summary>
        ///     Property for summary of course
        /// </summary>
        [Required(ErrorMessage = "Summary is require")]
        public string? Summary { get; set; }

        /// <summary>
        ///     Property for duration of course (unit in minute)
        /// </summary>
        [Required(ErrorMessage = "Duration is require")]
        public int Duration { get; set; }

        /// <summary>
        ///     Property for description of course
        /// </summary>
        [Required(ErrorMessage = "Duration is require")]
        public string? Description { get; set; }

        /// <summary>
        ///     Property for available slot of course
        /// </summary>
        [Required(ErrorMessage = "Duration is require")]
        public int AvailableSlot { get; set; }

        /// <summary>
        ///     Property for max slot of course
        /// </summary>
        [Required(ErrorMessage = "Duration is require")]
        public int MaxSlot { get; set; }

        /// <summary>
        ///     Property for rating of course
        /// </summary>
        [Required(ErrorMessage = "Duration is require")]
        public float Rating { get; set; }

        /// <summary>
        ///     Property for feefback of course
        /// </summary>
        [Required(ErrorMessage = "Duration is require")]
        public string? Feedback { get; set; }

        /// <summary>
        ///     Property for rating count of course
        /// </summary>
        [Required(ErrorMessage = "Duration is require")]
        public int RatingCount { get; set; }

        /// <summary>
        ///     Property for cost of course
        /// </summary>
        [Required(ErrorMessage = "Duration is require")]
        public decimal Cost { get; set; }

        /// <summary>
        ///     Property for subject id
        /// </summary>
        [Required(ErrorMessage = "Duration is require")]
        public Guid SubjectId { get; set; }

        /// <summary>
        ///     Property for user id is instructor
        /// </summary>
        [Required(ErrorMessage = "Duration is require")]
        public Guid InstructorId { get; set; }

        public List<Guid> ScheduleIds { get; set; }
    }
}
