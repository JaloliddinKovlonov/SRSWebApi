using Microsoft.EntityFrameworkCore;
using SRSWebApi.Data;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRSWebApi.Repository
{
	public class ScheduleRepository : IScheduleRepository
	{
		private readonly SrsContext _context;

		public ScheduleRepository(SrsContext context)
		{
			_context = context;
		}

		public bool CreateSchedule(ScheduleDTO schedule)
		{
			Schedule scheduleToCreate = new Schedule
			{
				StartTime = schedule.StartTime,
				EndTime = schedule.EndTime,
				DayOfWeek = schedule.DayOfWeek,
				RoomNo = schedule.RoomNo,
				CourseId = schedule.CourseId,
			};

			_context.Schedules.Add(scheduleToCreate);
			return Save();
		}

		public bool DeleteSchedule(int id)
		{
			var schedule = _context.Schedules.Where(p => p.ScheduleId == id).FirstOrDefault();
			_context.Schedules.Remove(schedule);
			return Save();
		}

		public Schedule GetScheduleById(int id)
		{
			return _context.Schedules
				.Include(s => s.Course)
				.Where(p => p.ScheduleId == id)
				.FirstOrDefault();
		}

		public ICollection<ScheduleGetDTO?> GetScheduleByStudentId(int studentId)
		{
			ICollection<StudentCourse> studentCourses = _context.StudentCourses
				.Where(sc => sc.StudentId == studentId && sc.IsCompleted == 0)
				.ToList();

			List<int> courseIds = studentCourses.Select(sc => sc.CourseId).ToList();

			ICollection<Schedule> schedules = _context.Schedules
				.Where(s => courseIds.Contains((int)s.CourseId))
				.Include(s => s.Course)
				.ThenInclude(c => c.Professor)
				.ToList();

			ICollection<ScheduleGetDTO?> schedulesToReturn = schedules.Select(s => new ScheduleGetDTO
			{
				StartTime = s.StartTime,
				EndTime = s.EndTime,
				DayOfWeek = s.DayOfWeek,
				RoomNo = s.RoomNo,
				CourseId = s.CourseId,
				CourseName = s.Course?.CourseName,
				CourseCode = s.Course?.CourseCode,
				ProfessorName = s.Course?.Professor != null ? $"{s.Course.Professor.FirstName} {s.Course.Professor.LastName}" : null
			}).ToList();

			return schedulesToReturn;
		}

		public ICollection<ScheduleGetDTO?> GetScheduleByProfessorId(int professorId)
		{
			ICollection<Schedule> schedules = _context.Schedules
				.Where(s => s.Course.ProfessorId == professorId)
				.Include(s => s.Course)
				.ThenInclude(c => c.Professor)
				.ToList();

			ICollection<ScheduleGetDTO?> schedulesToReturn = schedules.Select(s => new ScheduleGetDTO
			{
				StartTime = s.StartTime,
				EndTime = s.EndTime,
				DayOfWeek = s.DayOfWeek,
				RoomNo = s.RoomNo,
				CourseId = s.CourseId,
				CourseName = s.Course?.CourseName,
				CourseCode = s.Course?.CourseCode,
				ProfessorName = s.Course?.Professor != null ? $"{s.Course.Professor.FirstName} {s.Course.Professor.LastName}" : null
			}).ToList();

			return schedulesToReturn;
		}

		public ICollection<ScheduleGetDTO> GetSchedules()
		{
			ICollection<Schedule> schedules = _context.Schedules
				.Include(s => s.Course)
				.ThenInclude(c => c.Professor)
				.ToList();

			ICollection<ScheduleGetDTO> schedulesToReturn = schedules.Select(s => new ScheduleGetDTO
			{
				StartTime = s.StartTime,
				EndTime = s.EndTime,
				DayOfWeek = s.DayOfWeek,
				RoomNo = s.RoomNo,
				CourseId = s.CourseId,
				CourseName = s.Course?.CourseName,
				CourseCode = s.Course?.CourseCode,
				ProfessorName = s.Course?.Professor != null ? $"{s.Course.Professor.FirstName} {s.Course.Professor.LastName}" : null
			}).ToList();

			return schedulesToReturn;
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0;
		}
	}
}
