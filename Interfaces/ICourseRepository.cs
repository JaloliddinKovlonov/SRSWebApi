using SRSWebApi.DTO;
using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
	public interface ICourseRepository
	{
		ICollection<Course> GetCourses();
		Course GetCourseById(int id);
		bool CreateCourse(CourseDTO course);
		bool DeleteCourse(int id);
		ICollection<AvailableCourseDTO> GetAvailableCoursesForStudent(int studentId);
	}
}
