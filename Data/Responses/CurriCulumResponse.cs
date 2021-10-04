using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class CurriCulumResponse : Response
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        /// <summary>
        ///     Property for name of curriculum
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     Property for course id
        /// </summary>
        public Guid CourseId { get; set; }
    }
}
