using SRSWebApi.DTO;
using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
	public interface ICourseRepository
	{
		ICollection<ListCourse> GetCourses();
		Course GetCourseById(int id);
		int CreateCourse(CourseDTO course);
		bool DeleteCourse(int id);
		public ICollection<AvailableCourseDTO> GetAvailableCoursesForStudent(int studentId, int? departmentId, int? facultyId);
        public ICollection<ListCourse> GetCoursesByDepartmentId(int departmentId);
		public bool IsReferenceCorrect(CourseDTO course);

	}
}
