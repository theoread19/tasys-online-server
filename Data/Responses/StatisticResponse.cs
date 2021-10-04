using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class StatisticResponse : Response
    {
        public int CountLeaner { get; set; }

        public int CountInstructor { get; set; }

        public int CountCourse { get; set; }
    }
}
