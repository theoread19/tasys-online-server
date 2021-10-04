using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for question
    /// </summary>
    public class QuestionTable  : BaseTable
    {
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

        /// <summary>
        ///     Property for test table,
        ///     one to many
        /// </summary>
        public TestTable? Test { get; set; }

        /// <summary>
        ///     List for answer table,
        ///     many to one
        /// </summary>
        public ICollection<AnswerTable>? Answers { get; set; }

    }
}
