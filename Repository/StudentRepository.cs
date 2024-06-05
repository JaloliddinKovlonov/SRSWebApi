using Microsoft.EntityFrameworkCore;
using SRSWebApi.Data;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;
using System.Linq;

namespace SRSWebApi.Repository
{
	public class StudentRepository : IStudentRepository
	{
		private readonly SrsContext _context;

		public StudentRepository(SrsContext context)
		{
			_context = context;
		}

		public bool CreateStudent(StudentCreateDTO studentCreateDTO)
		{
			var user = new User
			{
				UserName = $"{studentCreateDTO.FirstName} {studentCreateDTO.LastName}",
				Email = studentCreateDTO.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(studentCreateDTO.Password),
				CreatedOn = DateTime.UtcNow,
				ModifiedOn = DateTime.UtcNow,
				IsActive = 1,
				RoleId = 4
			};

			_context.Users.Add(user);
			_context.SaveChanges();

			var student = new Student
			{
				FirstName = studentCreateDTO.FirstName,
				LastName = studentCreateDTO.LastName,
				DepartmentId = studentCreateDTO.DepartmentId,
                AdvisorId = studentCreateDTO.AdvisorId,
                AdmissionDate = DateTime.UtcNow,
				UserId = user.UserId
			};

			_context.Students.Add(student);
			return Save();
		}

		public bool DeleteStudent(int id)
		{
			var student = _context.Students.FirstOrDefault(p => p.StudentId == id);
			if (student != null)
			{
				_context.Students.Remove(student);
				_context.SaveChanges();
				return true;
			}
			return false;
		}

		public Student GetStudentById(int id)
		{
			return _context.Students
                .Include(s => s.Advisor)
                .FirstOrDefault(p => p.StudentId == id);
		}

		public ICollection<Student> GetStudents()
		{
			return _context.Students
                .Include(s => s.Advisor)
                .ToList();
		}

		public ICollection<Student> GetStudentsByDepartmentId(int departmentId)
		{
			return _context.Students
                .Include(s => s.Advisor)
				.Where(s => s.DepartmentId == departmentId)
				.ToList();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0;
		}
	}
}
