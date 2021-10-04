using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Responses
{
    public class UserInfoResponse : Response
    {
        /// <summary>
        ///     Property for id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Property for created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        ///     Property for modified date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        ///     Property for fullname of user information
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        ///     Property for gender of user information
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        ///     Property for address of user information
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        ///     Property for date of birth of user information
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        ///     Property for phone of user information
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        ///     Property for email of user information
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        ///     Property for user account id
        /// </summary>
        public Guid UserAccountId { get; set; }

        /// <summary>
        ///     Property for bio of user information
        /// </summary>
        public String? Bio { get; set; }

    }
}
