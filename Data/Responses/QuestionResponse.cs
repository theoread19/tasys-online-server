using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class QuestionResponse : Response
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        ///     Property for content of question
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        ///     Property for score of question
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        ///     Property for total correct answer of question
        /// </summary>
        public int TotalCorrectAnswer { get; set; }

        /// <summary>
        ///     Property for test id
        /// </summary>
        public Guid TestId { get; set; }

        public List<AnswerResponse> AnswerResponses { get; set; }
    }
}
