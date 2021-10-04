using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table.Identity;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IIdentityService
    {
        public Task CreateIdentityUserAccountAsync(IdentityUserAccount identityUserAccount, string role);

        public Task UpdateIdentityUserAccountAsync(IdentityUserAccount identityUserAccount);

        public Task DeleteIdentityUserAccountAsync(string id);

        public Task ChangeRoleIdentityUserAccountAsync(IdentityUserAccount identityUserAccount, string role);
    }
}
