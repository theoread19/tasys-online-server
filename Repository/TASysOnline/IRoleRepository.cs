﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Repository.TASysOnline
{
    public interface IRoleRepository : IRepository<RoleTable>
    {
        public Task<RoleTable> GetRoleByName(string name);
    }
}
