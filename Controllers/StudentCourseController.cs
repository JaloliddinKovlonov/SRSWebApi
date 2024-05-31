using Microsoft.AspNetCore.Mvc;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;

namespace SRSWebApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class StudentCourseController : Controller
	{
		private readonly IStudentCourseRepository _studentCourseRepository;

		public StudentCourseController(IStudentCourseRepository studentCourseRepository)
		{
			_studentCourseRepository = studentCourseRepository;
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpGet]
		[ProducesResponseType(200)]
		public IActionResult GetStudentCourses()
		{
			var studentCourses = _studentCourseRepository.GetStudentCourses();
			return Ok(studentCourses);
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		public IActionResult GetStudentCourseById(int id)
		{
			var studentCourse = _studentCourseRepository.GetStudentCourseById(id);
			if (studentCourse == null) return NotFound();
			return Ok(studentCourse);
		}

		[HttpGet("student/{studentId}")]
		[ProducesResponseType(200)]
		public IActionResult GetStudentCoursesByStudentId(int studentId)
		{
			var studentCourses = _studentCourseRepository.GetStudentCoursesByStudentId(studentId);
			if (studentCourses == null || !studentCourses.Any()) return NotFound();
			return Ok(studentCourses);
		}

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateStudentCourses([FromBody] StudentCourseCreateDTO studentCourse)
        {
            var result = _studentCourseRepository.CreateStudentCourses(studentCourse);
            return Ok(result);
        }

        [HttpPut("{id}")]
		[ProducesResponseType(200)]
		public IActionResult UpdateStudentCourse(int id, [FromBody] StudentCourseUpdateDTO studentCourse)
		{
			var result = _studentCourseRepository.UpdateStudentCourse(id, studentCourse);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		public IActionResult DeleteStudentCourse(int id)
		{
			var result = _studentCourseRepository.DeleteStudentCourse(id);
			return Ok(result);
		}
	}
}
