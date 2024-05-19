using Microsoft.EntityFrameworkCore;
using SRSWebApi.Data;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;

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
			return _context.Courses.
				Include(s => s.Semester).
				Include(c => c.Professor).
				ToList();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
