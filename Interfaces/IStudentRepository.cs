using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
	public interface IStudentRepository
	{
		ICollection<Student> GetStudents();
	}
}
