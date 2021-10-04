using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IScheduleService
    {
        /*        public Task<Response> CreateSchedule(ScheduleRequest scheduleRequest);

                public Task<IEnumerable<ScheduleResponse>> GetScheduleByDayOfWeek(DayOfWeek day, Guid userId);

                public Task<IEnumerable<ScheduleResponse>> GetSchedule(Guid userId);*/

        public Task<IEnumerable<ScheduleResponse>> GetValidScheduleForUserId(Guid userId);
    }
}
