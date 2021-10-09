using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IRoleService
    {
        public Task<Response> CreateAsync(RoleRequest roleRequest);

        public Task<IEnumerable<RoleResponse>> GetAllRoleAsync();

        public Task<RoleResponse> FindByIdAsync(Guid Id);
    }
}
