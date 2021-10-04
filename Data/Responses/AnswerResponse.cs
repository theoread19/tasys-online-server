using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class AnswerResponse : Response
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        ///     Property for content of Answer
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        ///     Propert for is correct of Answer
        /// </summary>
        public bool IsCorrect { get; set; }

        /// <summary>
        ///     Property for question id
        /// </summary>
        public Guid QuestionId { get; set; }
    }
}
