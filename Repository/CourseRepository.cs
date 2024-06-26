﻿using Microsoft.EntityFrameworkCore;
using SRSWebApi.Data;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRSWebApi.Repository
{
	public class CourseRepository : ICourseRepository
	{
		private readonly SrsContext _context;

		public CourseRepository(SrsContext context)
		{
			_context = context;
		}

		public int CreateCourse(CourseDTO course)
		{
			Course courseToCreate = new Course
			{
				CourseCode = course.CourseCode,
				CourseName = course.CourseName,
				CourseDescription = course.CourseDescription,
				AcademicYear = course.AcademicYear,
				SemesterId = course.SemesterId,
				ProfessorId = course.ProfessorId,
				CreditHours = course.CreditHours,
				DepartmentId = course.DepartmentId,
				PrerequisiteCourseId = course.PrerequisiteCourseId,
				CreatedOn = DateTime.Now.ToString(),
			};

			_context.Courses.Add(courseToCreate);
            if (Save())
            {
                return courseToCreate.CourseId;
            }

            return 0;
        }

		public bool IsReferenceCorrect(CourseDTO course)
		{
			if (!_context.Semesters.Any(s => s.SemesterId == course.SemesterId) ||
				!_context.Professors.Any(p => p.ProfessorId == course.ProfessorId) ||
				!_context.Departments.Any(d => d.DepartmentId == course.DepartmentId) ||
				(course.PrerequisiteCourseId != null && !_context.Courses.Any(c => c.CourseId == course.PrerequisiteCourseId)))
			{
				return false;
			}

			return true;
		}
		public bool DeleteCourse(int id)
		{
			var course = _context.Courses.Where(p => p.CourseId == id).FirstOrDefault();
			_context.Courses.Remove(course);
			return Save();
		}

		public Course GetCourseById(int id)
		{
			return _context.Courses.Where(p => p.CourseId == id).FirstOrDefault();
		}

		public ICollection<ListCourse> GetCourses()
		{
			return _context.Courses
                .Include(c => c.Professor)
                .Select(c => new ListCourse
                {
                    CourseId = c.CourseId,
                    AcademicYear = c.AcademicYear,
                    SemesterId = c.SemesterId,
                    ProfessorId = c.ProfessorId,
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    CreditHours = c.CreditHours,
                    DepartmentId = c.DepartmentId,
                    PrerequisiteCourseId = c.PrerequisiteCourseId,
                    ProfessorName = c.Professor != null ? $"{c.Professor.FirstName} {c.Professor.LastName}" : string.Empty
                })
                .ToList();
        }

		public ICollection<AvailableCourseDTO> GetAvailableCoursesForStudent(int studentId, int? departmentId, int? facultyId)
		{
			var takenCourses = _context.StudentCourses
				.Where(sc => sc.Student.UserId == studentId && (sc.IsCompleted == 1 || sc.IsCompleted == 0))
				.Select(sc => sc.CourseId)
				.ToList();

			var availableCourses = _context.Courses
				.Include(c => c.Professor)
				.Include(s => s.Schedules)
				.ToList()
				.Where(c => (c.PrerequisiteCourseId == 0 || c.PrerequisiteCourseId == null || takenCourses.Contains((int)c.PrerequisiteCourseId)) && !takenCourses.Contains(c.CourseId))
				.Select(c => new AvailableCourseDTO
				{
					CourseId = c.CourseId,
					CourseCode = c.CourseCode,
					CourseName = c.CourseName,
					CourseDescription = c.CourseDescription,
					AcademicYear = c.AcademicYear,
					SemesterId = c.SemesterId,
					ProfessorId = c.ProfessorId,
					CreditHours = c.CreditHours,
					DepartmentId = c.DepartmentId,
					Schedule = c.Schedules.ToList(),
					ProfessorName = c.Professor != null ? $"{c.Professor.FirstName} {c.Professor.LastName}" : string.Empty,
				})
				.ToList();

			if (departmentId != null)
			{
				availableCourses = availableCourses.Where(c => c.DepartmentId == departmentId).ToList();
			}

			if (facultyId != null)
			{
				var departmentsInFaculty = _context.Departments.Where(d => d.FacultyId == facultyId).Select(d => d.DepartmentId).ToList();
				availableCourses = availableCourses.Where(c => departmentsInFaculty.Contains(c.DepartmentId)).ToList();
			}

			return availableCourses;
		}

        public ICollection<ListCourse> GetCoursesByDepartmentId(int departmentId)
        {
            var courses = _context.Courses
                .Include(c => c.Professor)
                .Where(c => c.DepartmentId == departmentId)
                .Select(c => new ListCourse
                {
                    CourseId = c.CourseId,
                    AcademicYear = c.AcademicYear,
                    SemesterId = c.SemesterId,
                    ProfessorId = c.ProfessorId,
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    CreditHours = c.CreditHours,
                    DepartmentId = c.DepartmentId,
                    PrerequisiteCourseId = c.PrerequisiteCourseId,
                    ProfessorName = c.Professor != null ? $"{c.Professor.FirstName} {c.Professor.LastName}" : string.Empty
                })
                .ToList();

            return courses;
        }

        public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
