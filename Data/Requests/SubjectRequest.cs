﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class SubjectRequest
    {

        public Guid Id { get; set; }

        /// <summary>
        ///     Property for name of subject request
        /// </summary>
        public string? Name { get; set; }
    }
}
