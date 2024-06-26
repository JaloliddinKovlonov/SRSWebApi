﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("department/{departmentId}")]
        [ProducesResponseType(200)]
        public IActionResult GetCoursesByDepartmentId(int departmentId)
        {
            var courses = _courseRepository.GetCoursesByDepartmentId(departmentId);
            return Ok(courses);
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
			if(!_courseRepository.IsReferenceCorrect(course))
			{
				return StatusCode(400, "You are refencing non existing data");
			}
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

		[HttpGet("available/{userId}")]
		[ProducesResponseType(200)]
		public IActionResult GetAvailableCoursesForStudent(int userId, int? departmentId, int? facultyId)
		{
			var availableCourses = _courseRepository.GetAvailableCoursesForStudent(userId, departmentId, facultyId);
			return Ok(availableCourses);
		}
	}
}
