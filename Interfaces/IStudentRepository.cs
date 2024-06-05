using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SRSWebApi.DTO;
using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
	public interface IStudentRepository
	{
		ICollection<Student> GetStudents();
		Student GetStudentById(int id);
		bool CreateStudent(StudentCreateDTO studentCreateDTO);
		bool DeleteStudent(int id);
		ICollection<Student> GetStudentsByDepartmentId(int departmentId);
	}

}