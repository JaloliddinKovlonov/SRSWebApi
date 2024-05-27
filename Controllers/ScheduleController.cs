using Microsoft.AspNetCore.Mvc;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using System.Collections.Generic;

namespace SRSWebApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ScheduleController : Controller
	{
		private readonly IScheduleRepository _scheduleRepository;

		public ScheduleController(IScheduleRepository scheduleRepository)
		{
			_scheduleRepository = scheduleRepository;
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpGet]
		[ProducesResponseType(200)]
		public IActionResult GetSchedules()
		{
			var schedules = _scheduleRepository.GetSchedules();
			return Ok(schedules);
		}

		[HttpPost]
		[ProducesResponseType(200)]
		public IActionResult CreateSchedule([FromBody] ScheduleDTO schedule)
		{
			var result = _scheduleRepository.CreateSchedule(schedule);
			return Ok(result);
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		public IActionResult GetScheduleById(int id)
		{
			var schedule = _scheduleRepository.GetScheduleById(id);
			if (schedule == null) return NotFound();
			return Ok(schedule);
		}

		[HttpGet("StudentId/{studentId}")]
		[ProducesResponseType(200)]
		public IActionResult GetScheduleByStudentId(int studentId)
		{
			var schedule = _scheduleRepository.GetScheduleByStudentId(studentId);
			if (schedule == null) return NotFound();
			return Ok(schedule);
		}

		[HttpGet("ProfessorId/{professorId}")]
		[ProducesResponseType(200)]
		public IActionResult GetScheduleByProfessorId(int professorId)
		{
			var schedule = _scheduleRepository.GetScheduleByProfessorId(professorId);
			if (schedule == null) return NotFound();
			return Ok(schedule);
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		public IActionResult DeleteSchedule(int id)
		{
			var result = _scheduleRepository.DeleteSchedule(id);
			return Ok(result);
		}
	}
}
