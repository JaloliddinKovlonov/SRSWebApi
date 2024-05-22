using Microsoft.AspNetCore.Mvc;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;

namespace SRSWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FacultyController: Controller
    {
        private readonly IFacultyRepository _facultyRepository;

        public FacultyController(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Faculties()
        {
            var faculties = _facultyRepository.Faculty();
            return Ok(faculties);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult GetFacultyById(int id)
        {
            var faculty = _facultyRepository.GetFacultyById(id);
            if (faculty == null)
            {
                return NotFound();
            }
            return Ok(faculty);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateFaculty([FromBody] FacultyDTO faculty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var facultyToCreate = new Faculty
            {
				FacultyName = faculty.FacultyName,
                FacultyCode = faculty.FacultyCode,
                Description = faculty.Description,

		};

            var success = _facultyRepository.CreateFaculty(facultyToCreate);
            if (!success)
            {
                return StatusCode(500, "Failed to create faculty.");
            }

            return Ok(200);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public IActionResult DeleteFaculty(int id)
        {
            var success = _facultyRepository.DeleteFaculty(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
