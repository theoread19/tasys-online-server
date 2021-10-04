using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IStatisticService
    {
        public Task<StatisticResponse> GetStatistics();
    }
}
