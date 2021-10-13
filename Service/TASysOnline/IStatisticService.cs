using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Data.Responses.StatisticResponses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IStatisticService
    {
        public Task<CourseStatisticResponse> GetCourseStatistic();

        public Task<InstructorStatisticResponse> GetInstructorStatistic();

        public Task<LearnerStatisticResponse> GetLearnerStatisticResponse();

        public Task<StreamSessionStatisticResponse> GetStreamSessionStatistic();
    }
}
