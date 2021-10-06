using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline.impl
{
    public class ScheduleRepository : BaseRepository<ScheduleTable>, IScheduleRepository
    {
        private TASysOnlineContext _context;

        public ScheduleRepository() : base(new TASysOnlineContext())
        {
            this._context = new TASysOnlineContext();
        }

        /*       public async Task<List<ScheduleTable>> FindScheduleByUserId(Guid userId)
               {
                   try
                   {
                       var tables = await this._context.ScheduleTables
                                                   .Where(w => w.UserAccountId == userId)
                                                   .ToListAsync();
                       return tables;
                   }
                   catch
                   {
                       return new List<ScheduleTable>();
                   }
               }

               public async Task<List<ScheduleTable>> FindScheduleByUserIdAndCourseId(Guid userId, Guid courseId)
               {
                   try
                   {
                       var tables = await this._context.ScheduleTables.Where(w => w.CourseId == courseId)
                                                   .Where(w => w.UserAccountId == userId)
                                                   .ToListAsync();

                       return tables;
                   }
                   catch
                   {
                       return new List<ScheduleTable>();
                   }
               }

               public async Task<List<ScheduleTable>> FindSchedulesByDayofWeekAndUserId(DayOfWeek dateOfWeek, Guid userId)
               {
                   try
                   {
                       var tables = await this._context.ScheduleTables.Where(w => w.DayOfWeek == ((int)dateOfWeek))
                                                   .Where(w => w.UserAccountId == userId)
                                                   .ToListAsync();

                       return tables;
                   }
                   catch
                   {
                       return new List<ScheduleTable>();
                   }
               }*/
    }
}
