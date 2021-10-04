using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class TestResultRequest
    {
        /// <summary>
        ///     Property for score of test result
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        ///     Property for test id
        /// </summary>
        public Guid TestId { get; set; }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }
    }
}
