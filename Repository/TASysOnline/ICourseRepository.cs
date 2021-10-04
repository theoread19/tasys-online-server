﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface ICourseRepository : IRepository<CourseTable>
    {
        public Task<CourseTable> FindByNameAsync(string name);
    }
}
