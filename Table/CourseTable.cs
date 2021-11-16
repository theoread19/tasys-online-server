using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Table
{
    /// <summary>
    ///     Class for Course
    /// </summary>
    public class CourseTable : BaseTable
    {
        /// <summary>
        ///     Property for name of course
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     Property for summary of course
        /// </summary>
        public string? Summary { get; set; }

        /// <summary>
        ///     Property for duration of course (unit in minute)
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        ///     Property for description of course
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///     Property for available slot of course
        /// </summary>
        public int AvailableSlot { get; set; } = 0;

        /// <summary>
        ///     Property for max slot of course
        /// </summary>
        public int MaxSlot { get; set; }

        /// <summary>
        ///     Property for cost of course
        /// </summary>
        public decimal Cost { get; set; }

        public CourseTable()
        {
            this.LearnerAccounts = new HashSet<UserAccountTable>();
            this.Carts = new HashSet<CartTable>();
            this.BillTables = new HashSet<BillTable>();
        }

        /// <summary>
        ///     Property for subject id
        /// </summary>
        public Guid SubjectId { get; set; } // here

        /// <summary>
        ///     Property for user id is instructor
        /// </summary>
        public Guid? InstructorId { get; set; }

        /// <summary>
        ///     Property for subject table,
        ///     one to many
        /// </summary>
        public SubjectTable? Subject { get; set; } // here

        /// <summary>
        ///     Property for user is instructor
        ///     one to many
        /// </summary>
        public UserAccountTable? InstructorAccount { get; set; }

        /// <summary>
        ///     List for lesson table
        ///     many to one
        /// </summary>
        public ICollection<LessonTable>? LessonTables { get; set; }

        /// <summary>
        ///     List of test table
        ///     many to one
        /// </summary>
        public ICollection<TestTable>? Tests { get; set; }

        /// <summary>
        ///     List of stream session table
        ///     many to one
        /// </summary>
        public ICollection<StreamSessionTable>? StreamSessionTables { get; set; }

        /// <summary>
        ///     List of post table
        ///     many to one
        /// </summary>
        public ICollection<PostTable>? PostTables { get; set; }

        /// <summary>
        ///     List for message in course
        ///     many to one
        /// </summary>
        public ICollection<MessageTable>? Message { get; set; }

        /// <summary>
        ///     List of user is learner
        ///     many to many
        /// </summary>
        public virtual ICollection<UserAccountTable> LearnerAccounts { get; set; }

        /// <summary>
        ///     List of cart table
        ///     many to many
        /// </summary>
        public virtual ICollection<CartTable> Carts { get; set; }

        /// <summary>
        ///     List for bill table
        ///     many to many
        /// </summary>
        public virtual ICollection<BillTable> BillTables { get; set; }
    }
}
