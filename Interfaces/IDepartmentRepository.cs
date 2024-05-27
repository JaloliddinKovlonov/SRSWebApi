using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
    public interface IDepartmentRepository
    {
        ICollection<Department> Departments();
        Department GetDepartmentById(int id);
        public List<Department> GetDepartmentByFacultyId(int id);

		bool CreateDepartment(Department department);
        bool DeleteDepartment(int id);
    }
}
