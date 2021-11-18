using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Context
{
    public class TASysOnlineContext : DbContext
    {

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
            modelBuilder.Entity<RoleTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Name)
                    .HasMaxLength(15)
                    .IsRequired();

            });

            modelBuilder.Entity<UserInfoTable>(e => {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.HasOne(o => o.UserAccount)
                    .WithOne(o => o.UserInfo)
                    .HasForeignKey<UserInfoTable>(fk => fk.UserAccountId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                e.Property(p => p.Bio)
                    .IsRequired()
                    .HasColumnType("text");

                e.Property(p => p.Gender)
                    .IsRequired()
                    .HasMaxLength(10);

                e.Property(p => p.Email)
                    .HasMaxLength(100);

                e.Property(p => p.Address)
                    .HasMaxLength(255);

                e.Property(p => p.DateOfBirth);

                e.Property(p => p.Phone)
                    .HasMaxLength(15);

            });

            modelBuilder.Entity<UserAccountTable>(e => {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                e.Property(p => p.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50);

                e.Property(p => p.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                e.Property(p => p.Status);

                e.HasOne(o => o.Role)
                    .WithMany(m => m.UserAccounts)
                    .HasForeignKey(fk => fk.RoleId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CartTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.TotalCourse);

                e.Property(p => p.TotalCost);

                e.HasOne(o => o.UserAccount)
                    .WithOne(o => o.Cart)
                    .HasForeignKey<CartTable>(fk => fk.UserAccountId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(m => m.Courses)
                    .WithMany(m => m.Carts);
            });

            modelBuilder.Entity<CourseTable>(e => {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(p => p.Summary)
                    .HasColumnType("Text")
                    .IsRequired();

                e.Property(p => p.Duration);

                e.Property(p => p.Description)
                    .HasColumnType("Text")
                    .IsRequired();

                e.Property(p => p.AvailableSlot);

                e.Property(p => p.MaxSlot);

                e.Property(p => p.Cost);

                e.HasOne(o => o.Subject)
                    .WithMany(m => m.Courses)
                    .HasForeignKey(fk => fk.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(o => o.InstructorAccount)
                    .WithMany(m => m.CoursesOfInstrucor)
                    .HasForeignKey(fk => fk.InstructorId)
                    .OnDelete(DeleteBehavior.SetNull);

                e.HasMany(m => m.LearnerAccounts)
                    .WithMany(m => m.CoursesOfLearner);

                e.HasMany(m => m.Carts)
                    .WithMany(m => m.Courses);
            });

            modelBuilder.Entity<AnswerTable>(e => {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Content)
                    .HasColumnType("text");

                e.Property(e => e.IsCorrect);

                e.HasOne(o => o.Question)
                    .WithMany(m => m.Answers)
                    .HasForeignKey(fk => fk.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BillTable>(e => {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Description)
                    .HasColumnType("text");

                e.Property(p => p.TotalCost);

                e.Property(p => p.TotalItem);

                e.HasOne(o => o.UserAccount)
                    .WithMany(m => m.BillTables)
                    .HasForeignKey(fk => fk.UserAccountId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(m => m.CourseTables)
                    .WithMany(m => m.BillTables);
            });

            modelBuilder.Entity<CartTable>(e => {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.TotalCourse);

                e.HasOne(o => o.UserAccount)
                    .WithOne(o => o.Cart)
                    .HasForeignKey<CartTable>(fk => fk.UserAccountId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CommentTable>(e => {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Content)
                    .HasColumnType("text")
                    .IsRequired();

                e.HasOne(o => o.Post)
                    .WithMany(m => m.Comments)
                    .HasForeignKey(fk => fk.PostId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(o => o.UserAccount)
                    .WithMany(m => m.Comments)
                    .HasForeignKey(fk => fk.UserAccountId)
                    .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<LessonTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                e.Property(p => p.Description)
                    .HasColumnType("text")
                    .IsRequired();

                e.Property(p => p.BackText)
                    .HasColumnType("text")
                    .IsRequired();

                e.Property(p => p.FrontText)
                    .HasColumnType("text")
                    .IsRequired();

                e.HasOne(o => o.CourseTables)
                    .WithMany(m => m.LessonTables)
                    .HasForeignKey(fk => fk.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);


            });

            modelBuilder.Entity<MediaTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Category)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(p => p.Container)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(p => p.FileSize)
                    .HasColumnType("numeric(20,0)")
                    .IsRequired();

                e.Property(p => p.FileType)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(p => p.FileName)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(p => p.SourceID)
                    .HasColumnType("nvarchar(100)");

                e.Property(p => p.Title)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(p => p.Url)
                    .HasMaxLength(255)
                    .IsRequired();

            });

            modelBuilder.Entity<MessageTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Content)
                    .IsRequired()
                    .HasColumnType("text");

                e.HasOne(o => o.Sender)
                    .WithMany(m => m.SentMessage)
                    .HasForeignKey(fk => fk.SenderId)
                    .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(o => o.Course)
                    .WithMany(m => m.Message)
                    .HasForeignKey(fk => fk.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PostLikeTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.HasOne(p => p.Post)
                    .WithMany(m => m.PostLikes)
                    .HasForeignKey(fk => fk.PostId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(p => p.UserAccount)
                    .WithMany(m => m.PostLikes)
                    .HasForeignKey(fk => fk.UserAccountId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<PostTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Title)
                    .HasColumnType("text")
                    .IsRequired();

                e.Property(p => p.Content)
                    .HasColumnType("text")
                    .IsRequired();

                e.HasOne(o => o.UserAccount)
                    .WithMany(m => m.Posts)
                    .HasForeignKey(fk => fk.UserAccountId)
                    .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(o => o.CourseTable)
                    .WithMany(m => m.PostTables)
                    .HasForeignKey(fk => fk.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<QuestionTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Content)
                    .HasColumnType("text")
                    .IsRequired();

                e.Property(p => p.Score);

                e.Property(p => p.TotalCorrectAnswer);

                e.HasOne(o => o.Test)
                    .WithMany(m => m.Questions)
                    .HasForeignKey(fk => fk.TestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StreamSessionTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.StartTime)
                    .IsRequired();

                e.Property(p => p.EndTime)
                    .IsRequired();

                e.HasOne(o => o.Creator)
                    .WithMany(m => m.StreamSessionsCreated)
                    .HasForeignKey(fk => fk.CreatorId)
                    .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(o => o.CourseTable)
                    .WithMany(m => m.StreamSessionTables)
                    .HasForeignKey(fk => fk.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SubjectTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Name)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            modelBuilder.Entity<TestResultTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Score);

                e.HasOne(o => o.Test)
                    .WithMany(m => m.TestResults)
                    .HasForeignKey(fk => fk.TestId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(o => o.UserAccount)
                    .WithMany(m => m.TestResultTables)
                    .HasForeignKey(fk => fk.UserAccountId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<TestTable>(e =>
            {
                e.HasKey(k => k.Id);

                e.Property(p => p.Id)
                       .ValueGeneratedOnAdd()
                       .HasAnnotation("Relational:ColumnType", "nvarchar(100)")
                       .HasAnnotation("Relational:GeneratedValueSql", "newid()");

                e.Property(p => p.CreatedDate)
                   .IsRequired();

                e.Property(p => p.ModifiedDate);

                e.Property(p => p.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                e.Property(p => p.Description)
                    .IsRequired()
                    .HasColumnType("text");

                e.Property(p => p.AllocatedTime);

                e.Property(p => p.Deadline);

                e.Property(p => p.TotalQuestions);

                e.Property(p => p.MaxScore);

                e.Property(p => p.MaxAttempt);

                e.HasOne(o => o.Course)
                    .WithMany(m => m.Tests)
                    .HasForeignKey(fk => fk.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
        }

        public virtual DbSet<UserAccountTable> UserAccountTables { get; private set; } = null!;
        public virtual DbSet<UserInfoTable> UserInfoTables { get; private set; } = null!;
        public virtual DbSet<RoleTable> RoleTables { get; private set; } = null!;
        public virtual DbSet<CourseTable> CourseTables { get; private set; } = null!;
        public virtual DbSet<AnswerTable> AnswerTables { get; private set; } = null!;
        public virtual DbSet<BillTable> BillTables { get; private set; } = null!;
        public virtual DbSet<CartTable> CartTables { get; private set; } = null!;
        public virtual DbSet<CommentTable> CommentTables { get; private set; } = null!;
        public virtual DbSet<LessonTable> LessonTables { get; private set; } = null!;
        public virtual DbSet<MediaTable> MediaTables { get; private set; } = null!;
        public virtual DbSet<MessageTable> MessageTables { get; private set; } = null!;
        public virtual DbSet<PostLikeTable> PostLikeTables { get; private set; } = null!;
        public virtual DbSet<PostTable> PostTables { get; private set; } = null!;
        public virtual DbSet<QuestionTable> QuestionTables { get; private set; } = null!;
        public virtual DbSet<StreamSessionTable> StreamSessionTables { get; private set; } = null!;
        public virtual DbSet<SubjectTable> SubjectTables { get; private set; } = null!;
        public virtual DbSet<TestResultTable> TestResultTables { get; private set; } = null!;
        public virtual DbSet<TestTable> TestTables { get; private set; } = null!;

        
    }
}
