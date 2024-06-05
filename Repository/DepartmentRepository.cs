using SRSWebApi.Data;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;

namespace SRSWebApi.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly SrsContext _context;
        public DepartmentRepository(SrsContext context)
        {
            _context = context;
        }

        public bool CreateDepartment(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteDepartment(int id)
        {
            var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public ICollection<Department> Departments()
        {
            return _context.Departments.ToList();
        }

        public List<Department> GetDepartmentByFacultyId(int id)
        {
            return _context.Departments.Where(d => d.FacultyId == id).ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return _context.Departments.FirstOrDefault(d => d.DepartmentId == id);
        }
    }
}
