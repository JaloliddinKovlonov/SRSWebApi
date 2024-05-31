using SRSWebApi.DTO;
using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
	public interface ICourseRepository
	{
		ICollection<Course> GetCourses();
		Course GetCourseById(int id);
		int CreateCourse(CourseDTO course);
		bool DeleteCourse(int id);
		public ICollection<AvailableCourseDTO> GetAvailableCoursesForStudent(int studentId, int? departmentId, int? facultyId);
        public ICollection<CourseDTO> GetCoursesByDepartmentId(int departmentId);
		public bool IsReferenceCorrect(CourseDTO course);

	}
}
