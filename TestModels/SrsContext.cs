using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.TestModels;

public partial class SrsContext : DbContext
{
    public SrsContext()
    {
    }

    public SrsContext(DbContextOptions<SrsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Advisor> Advisors { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    public virtual DbSet<StudentGrade> StudentGrades { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=SRS.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseId).ValueGeneratedNever();

            entity.HasOne(d => d.Department).WithMany(p => p.Courses).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasOne(d => d.Faculty).WithMany(p => p.Departments).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Professors).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.Property(e => e.SemesterId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Students).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.Property(e => e.StudentCourseId).ValueGeneratedNever();
            entity.Property(e => e.IsCompleted).HasDefaultValue(0);

            entity.HasOne(d => d.Course).WithMany(p => p.StudentCourses).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Student).WithMany(p => p.StudentCourses).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<StudentGrade>(entity =>
        {
            entity.Property(e => e.GradeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Salt).HasDefaultValue("");

            entity.HasOne(d => d.Role).WithMany(p => p.Users).OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
