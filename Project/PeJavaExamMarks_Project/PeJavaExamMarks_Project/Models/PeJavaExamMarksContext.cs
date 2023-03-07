using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace PeJavaExamMarksProject.Models
{
    public partial class PeJavaExamMarksContext : DbContext
    {
        public PeJavaExamMarksContext()
        {
        }

        public PeJavaExamMarksContext(DbContextOptions<PeJavaExamMarksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassAccount> ClassAccounts { get; set; }
        public virtual DbSet<ScoreStudent> ScoreStudents { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("DbConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassDescription).HasMaxLength(100);

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ClassAccount>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.ClassId })
                    .HasName("PK_Class");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ClassAccounts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassAccounts_Accounts");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassAccounts)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassAccounts_Classes");
            });

            modelBuilder.Entity<ScoreStudent>(entity =>
            {
                entity.HasKey(e => new { e.ClassId, e.StudentId })
                    .HasName("PK_ScoreStudent");

                entity.Property(e => e.DateMark).HasColumnType("datetime");

                entity.Property(e => e.ExamCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ScoreDetails).HasColumnType("ntext");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ScoreStudents)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScoreStudents_Classes");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ScoreStudents)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScoreStudents_Students");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.RollNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StudentName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
