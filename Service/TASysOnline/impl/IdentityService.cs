using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Context;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Table.Identity;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class IdentityService : IIdentityService
    {

        private readonly UserManager<IdentityUserAccount> _userManager;
        private readonly UserAccountDbContext _userAccountDbContext;
        
        public IdentityService(UserManager<IdentityUserAccount> userManager, UserAccountDbContext userAccountDbContext)
        {
            this._userManager = userManager;
            this._userAccountDbContext = userAccountDbContext;
        }

        public async Task ChangeRoleIdentityUserAccountAsync(IdentityUserAccount identityUserAccount, string role)
        {
            var userRoles = await this._userAccountDbContext.UserRoles.Where(w => w.UserId == identityUserAccount.Id).FirstOrDefaultAsync();
            var roleIdentity = await this._userAccountDbContext.Roles.Where(w => w.Name == role).FirstOrDefaultAsync();            
            await this.DeleteUserRole(userRoles);
            userRoles.RoleId = roleIdentity.Id;
            await this._userAccountDbContext.UserRoles.AddAsync(userRoles);
            await this._userAccountDbContext.SaveChangesAsync();
        }

        public async Task CreateIdentityUserAccountAsync(IdentityUserAccount identityUserAccount, string role)
        {
            await this._userManager.CreateAsync(identityUserAccount);
            await this._userManager.AddToRoleAsync(identityUserAccount, role);
        }

        public async Task DeleteIdentityUserAccountAsync(string id)
        {
            var user = await this._userAccountDbContext.Users.FindAsync(id);
            this._userAccountDbContext.Users.Remove(user);
            await this._userAccountDbContext.SaveChangesAsync();
        }

        public Task UpdateIdentityUserAccountAsync(IdentityUserAccount identityUserAccount)
        {
            var identity = this._userAccountDbContext.Users.Find(identityUserAccount.Id);
            identity.PasswordHash = identityUserAccount.PasswordHash;
            this._userAccountDbContext.Users.Update(identity);
            this._userAccountDbContext.SaveChanges();
            return Task.CompletedTask;
        }

        private async Task DeleteUserRole(IdentityUserRole<string> userRoles)
        {
            this._userAccountDbContext.Remove(userRoles);
            await this._userAccountDbContext.SaveChangesAsync();
        }
    }
}

