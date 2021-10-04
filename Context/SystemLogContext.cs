using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Context
{
    public class SystemLogContext : DbContext
    {
        public virtual DbSet<SystemLogTable> SystemLogTables { get; private set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                               .AddJsonFile("appsettings.json")
                                               .Build();

                //optionsBuilder.UseSqlServer(configuration.GetConnectionString("AzureConnection"));
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("LocalConnection"));
                //optionsBuilder.UseMySQL(configuration.GetConnectionString("MySqlConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemLogTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                        .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Message)
                    .IsRequired()
                    .HasColumnType("text");
            });
        }
    }
}
