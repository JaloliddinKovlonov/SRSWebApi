using Microsoft.AspNetCore.Mvc;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;

namespace SRSWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdvisorController : Controller
    {
        private readonly IAdvisorRepository _advisorRepository;

        public AdvisorController(IAdvisorRepository advisorRepository)
        {
            _advisorRepository = advisorRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetAdvisors()
        {
            var advisors = _advisorRepository.GetAdvisors();
            return Ok(advisors);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult GetAdvisorById(int id)
        {
            var advisor = _advisorRepository.GetAdvisorById(id);
            if (advisor == null) return NotFound();
            return Ok(advisor);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateAdvisor([FromBody] AdvisorCreateDTO advisor)
        {
            var result = _advisorRepository.CreateAdvisor(advisor);
            if (!result)
            {
                return BadRequest("Failed to create advisor.");
            }
            return Ok(result);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public IActionResult UpdateAdvisor(int id, [FromBody] AdvisorUpdateDTO advisor)
        {
            var result = _advisorRepository.UpdateAdvisor(id, advisor);
            return Ok(result);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public IActionResult DeleteAdvisor(int id)
        {
            var result = _advisorRepository.DeleteAdvisor(id);
            return Ok(result);
        }
    }
}
