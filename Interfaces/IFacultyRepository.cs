using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
    public interface IFacultyRepository
    {
        ICollection<Faculty> Faculty();
        Faculty GetFacultyById(int id);
        bool CreateFaculty(Faculty faculty);
        bool DeleteFaculty(int id);
    }
}
