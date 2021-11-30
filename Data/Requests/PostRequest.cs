﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class PostRequest
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     Property for title post
        /// </summary>
        [Required]
        public string? Title { get; set; }

        /// <summary>
        ///     Property for content of post
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        [Required]
        public Guid UserAccountId { get; set; }

        /// <summary>
        ///     Property for course id
        /// </summary>
        [Required]
        public Guid CourseId { get; set; }
    }
}
