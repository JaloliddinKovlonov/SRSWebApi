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
			return _context.Courses.ToList();
		}

		public ICollection<AvailableCourseDTO> GetAvailableCoursesForStudent(int studentId, int? departmentId, int? facultyId)
		{
			var takenCourses = _context.StudentCourses
				.Where(sc => sc.StudentId == studentId && (sc.IsCompleted == 1 || sc.IsCompleted == 0))
				.Select(sc => sc.CourseId)
				.ToList();

			var availableCourses = _context.Courses
				.Include(c => c.Professor)
				.Include(s => s.Schedules)
				.ToList()
				.Where(c => (c.PrerequisiteCourseId == 0 || c.PrerequisiteCourseId == null || takenCourses.Contains(c.PrerequisiteCourseId)) && !takenCourses.Contains(c.CourseId))
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

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
