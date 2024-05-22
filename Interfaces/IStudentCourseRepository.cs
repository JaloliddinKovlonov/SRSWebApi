using SRSWebApi.DTO;
using SRSWebApi.Models;
using System.Collections.Generic;

namespace SRSWebApi.Interfaces
{
	public interface IStudentCourseRepository
	{
		ICollection<StudentCourseGetDTO> GetStudentCourses();
		StudentCourseGetDTO GetStudentCourseById(int id);
		bool CreateStudentCourse(StudentCourseCreateDTO studentCourse);
		bool UpdateStudentCourse(int id, StudentCourseUpdateDTO studentCourse);
		bool DeleteStudentCourse(int id);
		bool Save();
	}
}
