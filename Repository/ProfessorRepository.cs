using Microsoft.EntityFrameworkCore;
using SRSWebApi.Data;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRSWebApi.Repository
{
	public class ProfessorRepository : IProfessorRepository
	{
		private readonly SrsContext _context;

		public ProfessorRepository(SrsContext context)
		{
			_context = context;
		}

		public ICollection<Professor> GetProfessors()
		{
			return _context.Professors
				.Include(p => p.User)
				.ToList();
		}

		public Professor GetProfessorById(int id)
		{
			return _context.Professors
				.Include(p => p.User)
				.FirstOrDefault(p => p.ProfessorId == id);
		}

		public int CreateProfessor(ProfessorCreateDTO professorDTO)
		{
			var user = new User
			{
				UserName = $"{professorDTO.FirstName} {professorDTO.LastName}",
				Email = professorDTO.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(professorDTO.Password),
				CreatedOn = DateTime.UtcNow,
				ModifiedOn = DateTime.UtcNow,
				IsActive = 1,
				RoleId = 3
			};

			_context.Users.Add(user);
			_context.SaveChanges();

			var professor = new Professor
			{
				FirstName = professorDTO.FirstName,
				LastName = professorDTO.LastName,
				DepartmentId = professorDTO.DepartmentId,
				UserId = user.UserId
			};

			_context.Professors.Add(professor);
			if (Save())
			{
				return professor.ProfessorId;
			}

			return 0;
		}

		public bool UpdateProfessor(int id, ProfessorUpdateDTO professorDTO)
		{
			var professor = _context.Professors.FirstOrDefault(p => p.ProfessorId == id);
			if (professor == null) return false;

			professor.FirstName = professorDTO.FirstName;
			professor.LastName = professorDTO.LastName;
			professor.DepartmentId = professorDTO.DepartmentId;
			professor.UserId = professorDTO.UserId;

			_context.Professors.Update(professor);
			return Save();
		}

		public bool DeleteProfessor(int id)
		{
			var professor = _context.Professors.FirstOrDefault(p => p.ProfessorId == id);
			if (professor == null) return false;

			_context.Professors.Remove(professor);
			return Save();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

    }
}
