using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for answer
    /// </summary>
    public class AnswerTable : BaseTable
    {

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

        /// <summary>
        ///     Property for question table
        ///     one to many
        /// </summary>
        public QuestionTable? Question { get; set; }
    }
}
