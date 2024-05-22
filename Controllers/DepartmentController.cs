using Microsoft.AspNetCore.Mvc;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;

namespace SRSWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController: Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Departments()
        {
            var departments = _departmentRepository.Departments();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpGet("faculty/{facultyId}")]
        [ProducesResponseType(200)]
        public IActionResult GetDepartmentByFacultyId(int facultyId)
        {
            var department = _departmentRepository.GetDepartmentByFacultyId(facultyId);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateDepartment([FromBody] DepartmentDTO department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Department departmentToCreate = new Department
            {
                DepartmentName = department.DepartmentName,
                DepartmentCode = department.DepartmentCode,
                Description = department.Description,
                FacultyId = department.FacultyId,
            };

            var result = _departmentRepository.CreateDepartment(departmentToCreate);
            if (!result)
            {
                return StatusCode(500, "Failed to create department.");
            }

			return Ok(result);
		}

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public IActionResult DeleteDepartment(int id)
        {
            var success = _departmentRepository.DeleteDepartment(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
