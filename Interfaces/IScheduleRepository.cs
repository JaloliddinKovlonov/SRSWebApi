using SRSWebApi.DTO;
using SRSWebApi.Models;
using System.Collections.Generic;

namespace SRSWebApi.Interfaces
{
	public interface IScheduleRepository
	{
		ICollection<ScheduleGetDTO> GetSchedules();
		Schedule GetScheduleById(int id);
		bool CreateSchedule(ScheduleDTO schedule);
		bool DeleteSchedule(int id);
		bool Save();
		ICollection<ScheduleGetDTO?> GetScheduleByStudentId(int studentId);
	}
}
