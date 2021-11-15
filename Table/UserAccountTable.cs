using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table.Identity;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for useraccount
    /// </summary>
    public class UserAccountTable : BaseTable
    {
        /// <summary>
        ///     Property for username of account
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        ///     Property for password of account
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        ///     Status for user of account
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     Property for display name of account
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        ///     Contructor of user account
        /// </summary>
        public UserAccountTable()
        {
            this.CoursesOfInstrucor = new HashSet<CourseTable>();
            this.CoursesOfLearner = new HashSet<CourseTable>();
            this.TestResults = new HashSet<TestResultTable>();
            this.PostLikes = new HashSet<PostLikeTable>();
        }

        /// <summary>
        ///     Property for role id of user
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        ///     Property for role table
        ///     one to many
        /// </summary>
        public RoleTable? Role { get; set; }

        /// <summary>
        ///     Property for user information table
        ///     one to zero one
        /// </summary>
        public UserInfoTable? UserInfo { get; set; }

        /// <summary>
        ///     Property for cart table
        ///     one to one
        /// </summary>
        public CartTable? Cart { get; set; }

        /// <summary>
        ///     List for bill table
        ///     many to one
        /// </summary>
        public ICollection<BillTable>? BillTables { get; set; }

        /// <summary>
        ///     List for message is sent
        ///     many to one
        /// </summary>
        public ICollection<MessageTable>? SentMessage { get; set; }

        /// <summary>
        ///     List for course of user is instructor
        ///     many to one
        /// </summary>
        public ICollection<CourseTable> CoursesOfInstrucor { get; set; }

        /// <summary>
        ///     List for post table
        ///     many to one
        /// </summary>
        public ICollection<PostTable>? Posts { get; set; }

        /// <summary>
        ///     List of comment table
        ///     many to one
        /// </summary>
        public ICollection<CommentTable>? Comments{ get; set; }

        /// <summary>
        ///     List of stream session created
        ///     many to one
        /// </summary>
        public ICollection<StreamSessionTable>? StreamSessionsCreated { get; set; }

        /// <summary>
        ///     List of test result table
        ///     many to one
        /// </summary>
        public ICollection<TestResultTable>? TestResultTables { get; set; }

        /// <summary>
        ///     List of post like table
        ///     many to many
        /// </summary>
        public virtual ICollection<PostLikeTable> PostLikes { get; set; }

        /// <summary>
        ///     List for course of user is learner
        ///     many to many
        /// </summary>
        public virtual ICollection<CourseTable> CoursesOfLearner { get; set; }

        /// <summary>
        ///     List for test result of user is learner
        ///     many to many
        /// </summary>
        public virtual ICollection<TestResultTable> TestResults { get; set; }
    }
}
