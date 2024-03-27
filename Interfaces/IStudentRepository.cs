using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
	public interface IStudentRepository
	{
		ICollection<Student> GetStudents();
		Student GetStudentById(int id);
		bool CreateStudent(Student student);
		bool DeleteStudent(int id);


	}
}
