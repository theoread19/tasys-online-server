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

        private readonly IUserAccountService _userAccountService;

        private readonly IMapper _mapper;

        public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper, IUserAccountService userAccountService)
        {
            this._scheduleRepository = scheduleRepository;
            this._mapper = mapper;
            this._userAccountService = userAccountService;
        }

        public async Task<IEnumerable<ScheduleResponse>> GetSchedule()
        {
            var tables = await this._scheduleRepository.GetAllAsync();
            var responses = this._mapper.Map<List<ScheduleTable>, List<ScheduleResponse>>(tables);
            return responses;
        }

        public async Task<ScheduleResponse> GetScheduleById(Guid id)
        {
            var table = await this._scheduleRepository.FindByIdAsync(id);
            var response = this._mapper.Map<ScheduleResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Fetching data successfully!";
            return response;
        }

        public async Task<IEnumerable<ScheduleResponse>> GetAllScheduleByUserId(Guid userId)
        {
            var user = await this._userAccountService.GetUserAccountEagerLoadCourse(userId);
            if (user.StatusCode != 200)
            {
                return new List<ScheduleResponse>();
            }

            var coursesOfUser = user.CourseResponses.ToList();
            List<ScheduleResponse> schedulesOfUser = new List<ScheduleResponse>();
            foreach (var course in coursesOfUser)
            {
                schedulesOfUser.AddRange(course.ScheduleResponses.ToList());
            }
            return schedulesOfUser;
        }

        public async Task<Response> CreateScheduleAsync(ScheduleRequest scheduleRequest)
        {
            var table = this._mapper.Map<ScheduleTable>(scheduleRequest);
            table.Id = Guid.NewGuid();
            table.CreatedDate = DateTime.UtcNow;

            await this._scheduleRepository.InsertAsync(table);
            await this._scheduleRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Create schedule successfully!" };
        }

        public async Task<Response> UpdateScheduleAsync(ScheduleRequest scheduleRequest)
        {
            var table = await this._scheduleRepository.FindByIdAsync(scheduleRequest.Id);
            table.ModifiedDate = DateTime.UtcNow;
            table.StartTime = scheduleRequest.StartTime;
            table.EndTime = scheduleRequest.EndTime;
            table.DayOfWeek = scheduleRequest.DayOfWeek;
            await this._scheduleRepository.UpdateAsync(table);
            await this._scheduleRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update schedule successfully!" };
        }

        public async Task<Response> DeleteScheduleAsync(Guid[] ids)
        {
            for (var i = 0; i < ids.Length; i++)
            {
                await this._scheduleRepository.DeleteAsync(ids[i]);
            }

            await this._scheduleRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Schedule successfully!"
            };
        }
    }
}
