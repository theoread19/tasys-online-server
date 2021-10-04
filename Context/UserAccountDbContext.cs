using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;
using TASysOnlineProject.Table.Identity;

namespace TASysOnlineProject.Context
{
    public class UserAccountDbContext : IdentityDbContext<IdentityUserAccount>
    {

        public UserAccountDbContext(DbContextOptions<UserAccountDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
    }
}
