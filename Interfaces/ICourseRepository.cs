using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
	public interface ICourseRepository
	{
		ICollection<Course> GetCourses();
		Course GetCourseById(int id);
		bool CreateCourse(Course course);
		bool DeleteCourse(int id);
	}
}
