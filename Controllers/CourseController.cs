using Microsoft.AspNetCore.Mvc;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;

namespace SRSWebApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CourseController : Controller
	{
		private readonly ICourseRepository _courseRepository;

		public CourseController(ICourseRepository courseRepository)
		{
			_courseRepository = courseRepository;
		}


		[HttpGet]
		[ProducesResponseType(200)]
		public IActionResult GetCourses()
		{
			var courses = _courseRepository.GetCourses();
			return Ok(courses);
		}

		[HttpPost]
		[ProducesResponseType(200)]
		public IActionResult CreateCourse([FromBody] CourseDTO course)
		{
			var result = _courseRepository.CreateCourse(course);
			return Ok(result);
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		public IActionResult GetCourseById(int id)
		{
			var course = _courseRepository.GetCourseById(id);
			if (course == null) return NotFound();
			return Ok(course);
		}

		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		public IActionResult DeleteCourse(int id)
		{
			var result = _courseRepository.DeleteCourse(id);
			return Ok(result);
		}

		[HttpGet("available/{studentId}")]
		[ProducesResponseType(200)]
		public IActionResult GetAvailableCoursesForStudent(int studentId, int? departmentId, int? facultyId)
		{
			var availableCourses = _courseRepository.GetAvailableCoursesForStudent(studentId, departmentId, facultyId);
			return Ok(availableCourses);
		}
	}
}
