using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Data.Const
{
    /// <summary>
    ///     Constants class for role
    /// </summary>
    public static class Roles
    {
        /// <summary>
        ///     Constant for admin role
        /// </summary>
        public const string Admin = "Admin";
        public const string AdminId = "45930e92-5d46-47e7-c109-08d96a2de700";
        /// <summary>
        ///     Constant for Instructor role
        /// </summary>
        public const string Instructor = "Instructor";
        public const string InstructorId = "d74f9159-fdfa-4f1b-51dd-08d96a2f08cd";

        /// <summary>
        ///     Constant for Learner role
        /// </summary>
        public const string Learner = "Learner";
        public const string LearnerId = "6b9afeb1-18a1-42bb-51de-08d96a2f08cd";

        public const string All = "Learner,Admin,Instructor";
    }
}
