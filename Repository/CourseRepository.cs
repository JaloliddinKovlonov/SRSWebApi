using SRSWebApi.Data;
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
        public bool CreateCourse(Course course)
		{
			_context.Courses.Add(course);
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

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
