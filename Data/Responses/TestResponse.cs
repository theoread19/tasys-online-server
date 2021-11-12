using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class TestResponse : Response
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        ///     Property for name of test
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     Property for description of test
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///     Property for allocated time of test
        /// </summary>
        public int AllocatedTime { get; set; }

        /// <summary>
        ///     Property for deadline of test
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        ///     Property for total questions of test
        /// </summary>
        public int TotalQuestions { get; set; }

        /// <summary>
        ///     Property for max score of test
        /// </summary>
        public int MaxScore { get; set; }

        /// <summary>
        ///     Property for max attempt for test
        /// </summary>
        public int MaxAttempt { get; set; }

        /// <summary>
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }

        public List<QuestionResponse> QuestionResponses { get; set; }
    }
}
