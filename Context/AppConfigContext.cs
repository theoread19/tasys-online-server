using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Context
{
    public class AppConfigContext : DbContext
    {
        public virtual DbSet<AppConfigTable> AppConfigTables { get; private set; } = null!;
        public virtual DbSet<ThemeTable> ThemeTables { get; private set; } = null!;
        public virtual DbSet<LocalizationTable> LocalizationTables { get; private set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                               .AddJsonFile("appsettings.json")
                                               .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("AzureConnection"));
                //optionsBuilder.UseSqlServer(configuration.GetConnectionString("LocalConnection"));
                //optionsBuilder.UseMySQL(configuration.GetConnectionString("MySqlConnection"));
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfigTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.PropertyName)
                    .HasMaxLength(50)
                    .IsRequired();

                e.Property(p => p.CreatedDate)
                    .IsRequired();

                e.Property(p => p.ModifiedDate);
            });

            modelBuilder.Entity<ThemeTable>(e => {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.MainAccept)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(p => p.DeviceType)
                    .IsRequired();

                e.Property(p => p.FontSize)
                    .IsRequired();

                e.Property(p => p.FrontFamily)
                    .IsRequired();

                e.Property(p => p.SecondaryAccent)
                    .IsRequired()
                    .HasMaxLength(100);

                e.HasOne(o => o.AppConfig)
                    .WithOne(o => o.Theme)
                    .HasForeignKey<ThemeTable>(fk => fk.AppConfigId);
            });

            modelBuilder.Entity<LocalizationTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.SystemLanguage)
                    .IsRequired()
                    .HasMaxLength(50);

                e.HasOne(o => o.AppConfig)
                    .WithOne(o => o.Localization)
                    .HasForeignKey<LocalizationTable>(fk => fk.AppConfigId);
                    
            });
        }

        
    }
}
