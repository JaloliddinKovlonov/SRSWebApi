using SRSWebApi.Data;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;

namespace SRSWebApi.Repository
{
        public class FacultyRepository : IFacultyRepository
        {
            private readonly SrsContext _context;

            public FacultyRepository(SrsContext context)
            {
            _context = context;
            }
            public bool CreateFaculty(Faculty faculty)
            {
                _context.Faculties.Add(faculty);
                _context.SaveChanges();
                return true;
        }

            public bool DeleteFaculty(int id)
            {
            var faculty = _context.Faculties.FirstOrDefault(f => f.FacultyId == id);
            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

            public ICollection<Faculty> Faculty()
            {
                return _context.Faculties.ToList();
            }

            public Faculty GetFacultyById(int id)
            {
            return _context.Faculties.FirstOrDefault(f => f.FacultyId == id);
        }
        }
}
