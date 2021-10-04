using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface IScheduleRepository : IRepository<ScheduleTable>
    {
/*        public Task<List<ScheduleTable>> FindScheduleByUserIdAndCourseId(Guid userId, Guid courseId);

        public Task<List<ScheduleTable>> FindSchedulesByDayofWeekAndUserId(DayOfWeek dateOfWeek, Guid userId);

        public Task<List<ScheduleTable>> FindScheduleByUserId(Guid userId);*/
    }
}
