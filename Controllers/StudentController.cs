using Microsoft.AspNetCore.Mvc;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;

namespace SRSWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController: Controller
    {
        IStudentRepository _studentRepository;
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
        public IActionResult CreateStudent([FromBody] Student student)
        {
            var result = _studentRepository.CreateStudent(student);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _studentRepository.DeleteStudent(id);
            return Ok(result);
        }


    }
}
