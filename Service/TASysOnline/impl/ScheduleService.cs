using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class ScheduleService : IScheduleService
    {

        private readonly IScheduleRepository _scheduleRepository;

        private readonly ICourseService _courseService;

        private readonly IUserAccountService _userAccountService;

        private readonly IMapper _mapper;

        public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper, ICourseService courseService, IUserAccountService userAccountService)
        {
            this._scheduleRepository = scheduleRepository;
            this._courseService = courseService;
            this._mapper = mapper;
            this._userAccountService = userAccountService;
        }

        public async Task<IEnumerable<ScheduleResponse>> GetValidScheduleForUserId(Guid userId)
        {
            var user = await this._userAccountService.GetUserAccountEagerLoadCourse(userId);
            var schedulesOfuser = user.CourseResponses.Select(s => s.ScheduleResponse).ToList();
            var schedules = await this._scheduleRepository.GetAllAsync();
            var scheduleResponses = this._mapper.Map<List<ScheduleTable>, List<ScheduleResponse>>(schedules);
            var responses = scheduleResponses.Except(schedulesOfuser);
            return responses;
        }
    }
}
