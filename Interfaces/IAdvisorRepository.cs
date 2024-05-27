using SRSWebApi.DTO;
using SRSWebApi.Models;
using System.Collections.Generic;

namespace SRSWebApi.Interfaces
{
    public interface IAdvisorRepository
    {
        ICollection<Advisor> GetAdvisors();
        Advisor GetAdvisorById(int id);
        bool CreateAdvisor(AdvisorCreateDTO advisor);
        bool UpdateAdvisor(int id, AdvisorUpdateDTO advisor);
        bool DeleteAdvisor(int id);
        bool Save();
    }
}
