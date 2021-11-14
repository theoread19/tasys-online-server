using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class CourseResponse : Response
    {

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        ///     Property for name of course
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     Property for summary of course
        /// </summary>
        public string? Summary { get; set; }

        /// <summary>
        ///     Property for duration of course (unit in minute)
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        ///     Property for description of course
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///     Property for available slot of course
        /// </summary>
        public int AvailableSlot { get; set; }

        /// <summary>
        ///     Property for max slot of course
        /// </summary>
        public int MaxSlot { get; set; }

        /// <summary>
        ///     Property for cost of course
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        ///     Property for subject id
        /// </summary>
        public Guid SubjectId { get; set; }

        /// <summary>
        ///     Property for user id is instructor
        /// </summary>
        public Guid? InstructorId { get; set; }

        public int TotalTest { get; set; }

        public int TotalLesson { get; set; }
    }
}
