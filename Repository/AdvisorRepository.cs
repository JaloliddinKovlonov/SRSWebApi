using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SRSWebApi.Data;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRSWebApi.Repository
{
    public class AdvisorRepository : IAdvisorRepository
    {
        private readonly SrsContext _context;
        private IProfessorRepository _professorRepository;
        private IUserRepository _userRepository;
        private IStudentRepository _studentRepository;
        public AdvisorRepository(SrsContext context, IProfessorRepository professorRepository, IUserRepository userRepository, IStudentRepository studentRepository)
        {
            _context = context;
            _professorRepository = professorRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }

        public ICollection<Advisor> GetAdvisors()
        {
            return _context.Advisors
                .Include(a => a.Professor)
                .ToList();
        }

        public Advisor GetAdvisorById(int id)
        {
            return _context.Advisors
                .Include(a => a.Professor)
                .Include(s => s.Students)
                .FirstOrDefault(a => a.AdvisorId == id);
        }

        public bool CreateAdvisor(AdvisorCreateDTO advisorDTO)
        {
            var advisor = new Advisor
            {
                ProfessorId = advisorDTO.ProfessorId
            };

            _context.Advisors.Add(advisor);
            _context.SaveChanges();

            Professor professor = _professorRepository.GetProfessorById((int)advisor.ProfessorId);
            User userToUpdate = _userRepository.GetUserByUserId(professor.UserId);
            if (userToUpdate != null)
            {
                userToUpdate.RoleId = 2;
            }

            return Save();
        }

        public bool UpdateAdvisor(int id, AdvisorUpdateDTO advisorDTO)
        {
            var advisor = _context.Advisors.FirstOrDefault(a => a.AdvisorId == id);
            if (advisor == null) return false;

            advisor.ProfessorId = advisorDTO.ProfessorId;
            _context.Advisors.Update(advisor);
            return Save();
        }

        public bool DeleteAdvisor(int id)
        {
            var advisor = _context.Advisors.FirstOrDefault(a => a.AdvisorId == id);
            if (advisor == null) return false;

            _context.Advisors.Remove(advisor);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
