using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class RoleService : IRoleService
    {

        private IRoleRepository _roleRepository;

        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(IRoleRepository roleRepository, RoleManager<IdentityRole> roleManager)
        {
            this._roleRepository = roleRepository;
            this._roleManager = roleManager;
        }

        public async Task<Response> CreateAsync(RoleRequest roleRequest)
        {
            await this._roleRepository.InsertAsync(new RoleTable {Name = roleRequest.Name, CreatedDate = roleRequest.CreatedDate});
            _ = this._roleRepository.SaveAsync();

            await this._roleManager.CreateAsync(new IdentityRole
            {
                Name = roleRequest.Name
            });

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Role was created!" };
        }

        public async Task<RoleResponse> FindByIdAsync(Guid Id)
        {
            var result = await this._roleRepository.FindByIdAsync(Id);

            if (result == null)
            {
                return new RoleResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ResponseMessage = "Role not found!"
                };
            }

            return new RoleResponse
            {
                Id = result.Id,
                Name = result.Name,
                CreatedDate = result.CreatedDate,
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Find role successfully!"
            };
        }

        public async Task<IEnumerable<RoleResponse>> GetAllRoleAsync()
        {
            var tables = await this._roleRepository.GetAllAsync();
            List<RoleResponse> respones = new List<RoleResponse>();

            foreach(var item in tables)
            {
                
                respones.Add(new RoleResponse {
                    Id = item.Id,
                    CreatedDate = item.CreatedDate,
                    Name = item.Name
                });
            }

            return respones;
        }
    }
}
