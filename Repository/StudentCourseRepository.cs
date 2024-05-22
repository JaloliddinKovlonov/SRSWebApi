using Microsoft.EntityFrameworkCore;
using SRSWebApi.Data;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRSWebApi.Repository
{
	public class StudentCourseRepository : IStudentCourseRepository
	{
		private readonly SrsContext _context;

		public StudentCourseRepository(SrsContext context)
		{
			_context = context;
		}

		public ICollection<StudentCourseGetDTO> GetStudentCourses()
		{
			return _context.StudentCourses
				.Include(sc => sc.Course)
				.Include(sc => sc.Student)
				.Select(sc => new StudentCourseGetDTO
				{
					StudentCourseId = sc.StudentCourseId,
					StudentId = sc.StudentId,
					CourseId = sc.CourseId,
					CourseName = sc.Course.CourseName,
					StudentName = $"{sc.Student.FirstName} {sc.Student.LastName}",
					IsApproved = sc.IsApproved,
					IsCompleted = sc.IsCompleted
				})
				.ToList();
		}

		public StudentCourseGetDTO GetStudentCourseById(int id)
		{
			return _context.StudentCourses
				.Include(sc => sc.Course)
				.Include(sc => sc.Student)
				.Where(sc => sc.StudentCourseId == id)
				.Select(sc => new StudentCourseGetDTO
				{
					StudentCourseId = sc.StudentCourseId,
					StudentId = sc.StudentId,
					CourseId = sc.CourseId,
					CourseName = sc.Course.CourseName,
					StudentName = $"{sc.Student.FirstName} {sc.Student.LastName}",
					IsApproved = sc.IsApproved,
					IsCompleted = sc.IsCompleted
				})
				.FirstOrDefault();
		}

		public bool CreateStudentCourse(StudentCourseCreateDTO studentCourseDTO)
		{
			StudentCourse studentCourse = new StudentCourse
			{
				StudentId = studentCourseDTO.StudentId,
				CourseId = studentCourseDTO.CourseId,
				IsApproved = studentCourseDTO.IsApproved
			};

			_context.StudentCourses.Add(studentCourse);
			return Save();
		}

		public bool UpdateStudentCourse(int id, StudentCourseUpdateDTO studentCourseDTO)
		{
			var studentCourse = _context.StudentCourses.FirstOrDefault(sc => sc.StudentCourseId == id);
			if (studentCourse == null) return false;

			studentCourse.IsApproved = studentCourseDTO.IsApproved;

			_context.StudentCourses.Update(studentCourse);
			return Save();
		}

		public bool DeleteStudentCourse(int id)
		{
			var studentCourse = _context.StudentCourses.FirstOrDefault(sc => sc.StudentCourseId == id);
			if (studentCourse == null) return false;

			_context.StudentCourses.Remove(studentCourse);
			return Save();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
