using Microsoft.AspNetCore.Mvc;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;

namespace SRSWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CourseDetailsController: Controller
    {
        ICourseDetailsRepository _courseDetailsRepository;
        public CourseDetailsController(ICourseDetailsRepository courseDetailsRepository)
        {
            _courseDetailsRepository = courseDetailsRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetAllCourseDetails()
        {
            var courseDetails = _courseDetailsRepository.GetAllCourseDetails();
            return Ok(courseDetails);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult GetCourseDetailsById(int id)
        {
            var courseDetails = _courseDetailsRepository.GetCourseDetailsById(id);
            if (courseDetails == null)
            {
                return NotFound();
            }
            return Ok(courseDetails);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddCourseDetails([FromBody] CourseDetail courseDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = _courseDetailsRepository.AddCourseDetails(courseDetails);
            if (!success)
            {
                return StatusCode(500, "Failed to add course details.");
            }

            return CreatedAtAction(nameof(GetCourseDetailsById), new { id = courseDetails.CourseDetailsId }, courseDetails);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public IActionResult UpdateCourseDetails(int id, [FromBody] CourseDetail courseDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = _courseDetailsRepository.UpdateCourseDetails(id, courseDetails);
            if (!success)
            {
                return StatusCode(500, "Failed to update course details.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public IActionResult DeleteCourseDetails(int id)
        {
            var success = _courseDetailsRepository.DeleteCourseDetails(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
