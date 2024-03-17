using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace SRSWebApi.Repository
{
	public class StudentRepository : IStudentRepository

	{
		private readonly SrsContext _context;

        public StudentRepository(SrsContext context)
        {
            _context = context;
        }

        public bool CreateStudent(Student student)
        {
            _context.Students.Add(student);
            return Save();


        }

        public bool DeleteStudent(int id)
        {
            var student = _context.Students.Where(p => p.StudentId == id).FirstOrDefault();
            if(student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return true;
            }
            else {
                return false; 
            }
        }

        public Student GetStudentById(int id)
        {
            return _context.Students.Where(p=> p.StudentId == id).FirstOrDefault();
        }

        public ICollection<Student> GetStudents()
		{
			return _context.Students.ToList();
		}

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
	}
}
