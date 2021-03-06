using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class DoTestRequest
    {
        [Required]
        public Guid TestId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public bool IsPractice { get; set; } = false;

        public List<QuestionRequest>? QuestionRequest { get; set; }
    }
}
