using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class DoTestRequest
    {
        public Guid TestId { get; set; }

        public Guid UserId { get; set; }

        public List<QuestionRequest>? QuestionRequest { get; set; }
    }
}
