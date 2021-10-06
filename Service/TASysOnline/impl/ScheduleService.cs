using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Enum;
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

        public async Task GenerateData()
        {
            var datas = new List<ScheduleTable>{
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Monday, StartTime = "7:00 AM", EndTime = "7:50 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Monday, StartTime = "7:50 AM", EndTime = "8:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Monday, StartTime = "8:50 AM", EndTime = "9:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Monday, StartTime = "9:50 AM", EndTime = "10:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Monday, StartTime = "10:40 AM", EndTime = "11:0 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Monday, StartTime = "13:30 PM", EndTime = "14:20 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Monday, StartTime = "14:20 PM", EndTime = "15:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Monday, StartTime = "15:20 PM", EndTime = "16:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Monday, StartTime = "18:20 PM", EndTime = "19:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Monday, StartTime = "19:10 PM", EndTime = "20:00 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Monday, StartTime = "20:20 PM", EndTime = "21:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Tuesday, StartTime = "7:00 AM", EndTime = "7:50 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Tuesday, StartTime = "7:50 AM", EndTime = "8:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Tuesday, StartTime = "8:50 AM", EndTime = "9:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Tuesday, StartTime = "9:50 AM", EndTime = "10:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Tuesday, StartTime = "10:40 AM", EndTime = "11:0 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Tuesday, StartTime = "13:30 PM", EndTime = "14:20 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Tuesday, StartTime = "14:20 PM", EndTime = "15:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Tuesday, StartTime = "15:20 PM", EndTime = "16:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Tuesday, StartTime = "18:20 PM", EndTime = "19:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Tuesday, StartTime = "19:10 PM", EndTime = "20:00 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Tuesday, StartTime = "20:20 PM", EndTime = "21:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Wednesday, StartTime = "7:00 AM", EndTime = "7:50 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Wednesday, StartTime = "7:50 AM", EndTime = "8:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Wednesday, StartTime = "8:50 AM", EndTime = "9:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Wednesday, StartTime = "9:50 AM", EndTime = "10:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Wednesday, StartTime = "10:40 AM", EndTime = "11:0 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Wednesday, StartTime = "13:30 PM", EndTime = "14:20 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Wednesday, StartTime = "14:20 PM", EndTime = "15:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Wednesday, StartTime = "15:20 PM", EndTime = "16:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Wednesday, StartTime = "18:20 PM", EndTime = "19:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Wednesday, StartTime = "19:10 PM", EndTime = "20:00 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Wednesday, StartTime = "20:20 PM", EndTime = "21:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Thursday, StartTime = "7:00 AM", EndTime = "7:50 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Thursday, StartTime = "7:50 AM", EndTime = "8:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Thursday, StartTime = "8:50 AM", EndTime = "9:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Thursday, StartTime = "9:50 AM", EndTime = "10:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Thursday, StartTime = "10:40 AM", EndTime = "11:0 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Thursday, StartTime = "13:30 PM", EndTime = "14:20 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Thursday, StartTime = "14:20 PM", EndTime = "15:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Thursday, StartTime = "15:20 PM", EndTime = "16:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Thursday, StartTime = "18:20 PM", EndTime = "19:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Thursday, StartTime = "19:10 PM", EndTime = "20:00 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Thursday, StartTime = "20:20 PM", EndTime = "21:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Friday, StartTime = "7:00 AM", EndTime = "7:50 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Friday, StartTime = "7:50 AM", EndTime = "8:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Friday, StartTime = "8:50 AM", EndTime = "9:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Friday, StartTime = "9:50 AM", EndTime = "10:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Friday, StartTime = "10:40 AM", EndTime = "11:0 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Friday, StartTime = "13:30 PM", EndTime = "14:20 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Friday, StartTime = "14:20 PM", EndTime = "15:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Friday, StartTime = "15:20 PM", EndTime = "16:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Friday, StartTime = "18:20 PM", EndTime = "19:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Friday, StartTime = "19:10 PM", EndTime = "20:00 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Friday, StartTime = "20:20 PM", EndTime = "21:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Saturday, StartTime = "7:00 AM", EndTime = "7:50 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Saturday, StartTime = "7:50 AM", EndTime = "8:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Saturday, StartTime = "8:50 AM", EndTime = "9:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Saturday, StartTime = "9:50 AM", EndTime = "10:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Saturday, StartTime = "10:40 AM", EndTime = "11:0 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Saturday, StartTime = "13:30 PM", EndTime = "14:20 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Saturday, StartTime = "14:20 PM", EndTime = "15:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Saturday, StartTime = "15:20 PM", EndTime = "16:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Saturday, StartTime = "18:20 PM", EndTime = "19:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Saturday, StartTime = "19:10 PM", EndTime = "20:00 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Saturday, StartTime = "20:20 PM", EndTime = "21:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Sunday, StartTime = "7:00 AM", EndTime = "7:50 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Sunday, StartTime = "7:50 AM", EndTime = "8:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Sunday, StartTime = "8:50 AM", EndTime = "9:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Sunday, StartTime = "9:50 AM", EndTime = "10:40 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Sunday, StartTime = "10:40 AM", EndTime = "11:0 AM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Sunday, StartTime = "13:30 PM", EndTime = "14:20 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Sunday, StartTime = "14:20 PM", EndTime = "15:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Sunday, StartTime = "15:20 PM", EndTime = "16:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Sunday, StartTime = "18:20 PM", EndTime = "19:10 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Sunday, StartTime = "19:10 PM", EndTime = "20:00 PM"},
                new ScheduleTable { Id = Guid.NewGuid(), DayOfWeek = (int)DaysOfWeek.Sunday, StartTime = "20:20 PM", EndTime = "21:10 PM"},
            };

            foreach (var data in datas)
            {
                await this._scheduleRepository.InsertAsync(data);
            }

            await this._scheduleRepository.SaveAsync();
        }

        public async Task<IEnumerable<ScheduleResponse>> GetSchedule()
        {
            var tables = await this._scheduleRepository.GetAllAsync();
            var responses = this._mapper.Map<List<ScheduleTable>, List<ScheduleResponse>>(tables);
            return responses;
        }

        public Task<IEnumerable<ScheduleResponse>> GetScheduleByDayOfWeek(DayOfWeek day, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ScheduleResponse> GetScheduleById(Guid id)
        {
            var table = await this._scheduleRepository.FindByIdAsync(id);
            var response = this._mapper.Map<ScheduleResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Fetching data successfully!";
            return response;
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
