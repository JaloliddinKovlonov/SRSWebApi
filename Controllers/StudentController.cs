using Microsoft.AspNetCore.Mvc;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;

namespace SRSWebApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class StudentController : Controller
	{
		private readonly IStudentRepository _studentRepository;

		public StudentController(IStudentRepository studentRepository)
		{
			_studentRepository = studentRepository;
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public IActionResult GetStudents()
		{
			var students = _studentRepository.GetStudents();
			return Ok(students);
		}

		[HttpPost]
		[ProducesResponseType(200)]
		public IActionResult CreateStudent([FromBody] StudentCreateDTO studentCreateDTO)
		{
			var result = _studentRepository.CreateStudent(studentCreateDTO);
			if (!result)
			{
				return BadRequest("Failed to create student.");
			}
			return Ok(result);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		public IActionResult GetStudentById(int id)
		{
			var student = _studentRepository.GetStudentById(id);
			if (student == null) return NotFound();
			return Ok(student);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		public IActionResult Delete(int id)
		{
			var result = _studentRepository.DeleteStudent(id);
			return Ok(result);
		}

		[HttpGet("department/{departmentId}")]
		[ProducesResponseType(200)]
		public IActionResult GetStudentsByDepartmentId(int departmentId)
		{
			var students = _studentRepository.GetStudentsByDepartmentId(departmentId);
			if (students == null || !students.Any()) return NotFound();
			return Ok(students);
		}
	}
}
