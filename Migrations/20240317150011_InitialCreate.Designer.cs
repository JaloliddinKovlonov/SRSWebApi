﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SRSWebApi.Models;

#nullable disable

namespace SRSWebApi.Migrations
{
    [DbContext(typeof(SrsContext))]
    [Migration("20240317150011_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("SRSWebApi.Models.Course", b =>
                {
                    b.Property<int>("CourseDetailsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeletedById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ModifiedById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.ToTable("Courses", "Academic");
                });

            modelBuilder.Entity("SRSWebApi.Models.CourseDetail", b =>
                {
                    b.Property<int>("CourseDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("CreditHours")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PrerequisiteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CourseDetailsId");

                    b.ToTable("CourseDetails", "Academic");
                });

            modelBuilder.Entity("SRSWebApi.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DepartmentCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("FacultyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DepartmentId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Departments", "Academic");
                });

            modelBuilder.Entity("SRSWebApi.Models.Faculty", b =>
                {
                    b.Property<int>("FacultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("FacultyCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("FacultyId");

                    b.ToTable("Faculties", "Academic");
                });

            modelBuilder.Entity("SRSWebApi.Models.Gender", b =>
                {
                    b.Property<int>("GenderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GenderName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.ToTable("Genders", "Core");
                });

            modelBuilder.Entity("SRSWebApi.Models.Nationality", b =>
                {
                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NationalityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NationalityName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.ToTable("Nationalities", "Core");
                });

            modelBuilder.Entity("SRSWebApi.Models.Professor", b =>
                {
                    b.Property<int>("ProfessorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProfessorId");

                    b.HasIndex("UserId");

                    b.ToTable("Professors", "Users");
                });

            modelBuilder.Entity("SRSWebApi.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("RoleId");

                    b.ToTable("Roles", "Auth");
                });

            modelBuilder.Entity("SRSWebApi.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AdmissionDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("AdvisorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int>("GenderId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("GraduateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int?>("MajorId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MinorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NationalityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudentId");

                    b.HasIndex("UserId");

                    b.ToTable("Students", "Users");
                });

            modelBuilder.Entity("SRSWebApi.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeletedById")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", "Auth");
                });

            modelBuilder.Entity("SRSWebApi.Models.Department", b =>
                {
                    b.HasOne("SRSWebApi.Models.Faculty", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId")
                        .IsRequired()
                        .HasConstraintName("FK_Departments_Faculties");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("SRSWebApi.Models.Professor", b =>
                {
                    b.HasOne("SRSWebApi.Models.User", "User")
                        .WithMany("Professors")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Professors_Users");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SRSWebApi.Models.Student", b =>
                {
                    b.HasOne("SRSWebApi.Models.User", "User")
                        .WithMany("Students")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Students_Users");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SRSWebApi.Models.User", b =>
                {
                    b.HasOne("SRSWebApi.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK_Users_Roles");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SRSWebApi.Models.Faculty", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("SRSWebApi.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SRSWebApi.Models.User", b =>
                {
                    b.Navigation("Professors");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
