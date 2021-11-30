﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Requests
{
    public class UserAccountRequest
    {

        public Guid Id { get; set; }

        /// <summary>
        ///     Property for username of authenticate request
        /// </summary>
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? DisplayName { get; set; }

        /// <summary>
        ///     Property for password of authenticate request
        /// </summary>
        [Required]
        public string? Password { get; set; }

        /// <summary>
        ///     Property for role id of authenticate request
        /// </summary>
        [Required]
        public Guid RoleId { get; set; }

    }
}
