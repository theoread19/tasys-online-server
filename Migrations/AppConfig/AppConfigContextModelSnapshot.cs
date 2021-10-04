﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TASysOnlineProject.Context;

namespace TASysOnlineProject.Migrations.AppConfig
{
    [DbContext(typeof(AppConfigContext))]
    partial class AppConfigContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TASysOnlineProject.Table.AppConfigTable", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(100)")
                        .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("AppConfigTables");
                });

            modelBuilder.Entity("TASysOnlineProject.Table.LocalizationTable", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(100)")
                        .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                    b.Property<string>("AppConfigId")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SystemLanguage")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AppConfigId")
                        .IsUnique();

                    b.ToTable("LocalizationTables");
                });

            modelBuilder.Entity("TASysOnlineProject.Table.ThemeTable", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(100)")
                        .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                    b.Property<string>("AppConfigId")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeviceType")
                        .HasColumnType("int");

                    b.Property<int>("FontSize")
                        .HasColumnType("int");

                    b.Property<string>("FrontFamily")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainAccept")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecondaryAccent")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AppConfigId")
                        .IsUnique();

                    b.ToTable("ThemeTables");
                });

            modelBuilder.Entity("TASysOnlineProject.Table.LocalizationTable", b =>
                {
                    b.HasOne("TASysOnlineProject.Table.AppConfigTable", "AppConfig")
                        .WithOne("Localization")
                        .HasForeignKey("TASysOnlineProject.Table.LocalizationTable", "AppConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppConfig");
                });

            modelBuilder.Entity("TASysOnlineProject.Table.ThemeTable", b =>
                {
                    b.HasOne("TASysOnlineProject.Table.AppConfigTable", "AppConfig")
                        .WithOne("Theme")
                        .HasForeignKey("TASysOnlineProject.Table.ThemeTable", "AppConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppConfig");
                });

            modelBuilder.Entity("TASysOnlineProject.Table.AppConfigTable", b =>
                {
                    b.Navigation("Localization");

                    b.Navigation("Theme");
                });
#pragma warning restore 612, 618
        }
    }
}
