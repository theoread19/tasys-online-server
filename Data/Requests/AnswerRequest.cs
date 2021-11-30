using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class AnswerRequest
    {
        public Guid Id { get; set; }
        /// <summary>
        ///     Property for content of Answer
        /// </summary>
        [Required]
        public string? Content { get; set; }

        /// <summary>
        ///     Propert for is correct of Answer
        /// </summary>
        [Required]
        public bool IsCorrect { get; set; }

        /// <summary>
        ///     Property for question id
        /// </summary>
        [Required]
        public Guid QuestionId { get; set; }

    }
}
