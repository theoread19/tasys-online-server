using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface IUserAccountRepository : IRepository<UserAccountTable>
    {
        public Task<UserAccountTable> FindByUsernameAsync(string username);

        public Task<int> CountByRole(Guid Role);

        public Task<UserAccountTable> GetUserAccountEagerLoadCourse(Guid userId);

        public Task AddCourseToLeaner(Guid userId, CourseTable course);
    }
}
