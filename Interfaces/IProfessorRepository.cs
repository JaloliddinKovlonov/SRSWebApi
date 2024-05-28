using SRSWebApi.DTO;
using SRSWebApi.Models;
using System.Collections.Generic;

namespace SRSWebApi.Interfaces
{
	public interface IProfessorRepository
	{
		ICollection<Professor> GetProfessors();
		Professor GetProfessorById(int id);
		int CreateProfessor(ProfessorCreateDTO professor);
		bool UpdateProfessor(int id, ProfessorUpdateDTO professor);
		bool DeleteProfessor(int id);
		bool Save();
	}
}
