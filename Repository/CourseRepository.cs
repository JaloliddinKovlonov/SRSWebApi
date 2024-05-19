using Microsoft.EntityFrameworkCore;
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

		public bool CreateCourse(CourseDTO course)
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
			return Save();
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

		public ICollection<Course> GetCourses()
		{
			return _context.Courses
				.Include(s => s.Semester)
				.Include(c => c.Professor)
				.ToList();
		}

		public ICollection<AvailableCourseDTO> GetAvailableCoursesForStudent(int studentId)
		{
			var takenCourses = _context.StudentCourses
				.Where(sc => sc.StudentId == studentId && (sc.IsCompleted == 1 || sc.IsCompleted == 0))
				.Select(sc => sc.CourseId)
				.ToList();

			var availableCourses = _context.Courses
				.Where(c => !takenCourses.Contains(c.CourseId))
				.Include(c => c.Professor)
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
					ProfessorName = c.Professor != null ? $"{c.Professor.FirstName} {c.Professor.LastName}" : string.Empty
				})
				.ToList();

			return availableCourses;
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
