using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class OperationFlag
    {
        public bool IsShowingLesson { get; set; } = false;

        public bool IsShowingQuestion { get; set; } = false;

        public bool IsShowingAnswer { get; set; } = false;

        public bool IsShowingCorrectAnswer { get; set; } = false;

        public bool IsShowingResult { get; set; } = false;

    }
}
