using Microsoft.AspNetCore.Mvc;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;
using SRSWebApi.Repository;

namespace SRSWebApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CourseController : Controller
	{
		ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

		[HttpGet]
		[ProducesResponseType(200)]
		public IActionResult GetCourses()
		{
			var students = _courseRepository.GetCourses();
			return Ok(students);
		}

		[HttpPost]
		[ProducesResponseType(200)]
		public IActionResult CreateCourse([FromBody] CourseDTO course)
		{
			

			var result = _courseRepository.CreateCourse(course);
			return Ok(result);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		public IActionResult GetCourseById(int id)
		{
			var student = _courseRepository.GetCourseById(id);
			if (student == null) return NotFound();
			return Ok(student);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		public IActionResult DeleteCourse(int id)
		{
			var result = _courseRepository.DeleteCourse(id);
			return Ok(result);
		}
	}
}
