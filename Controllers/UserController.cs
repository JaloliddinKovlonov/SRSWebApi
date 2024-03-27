using Microsoft.AspNetCore.Mvc;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;
using SRSWebApi.Repository;

namespace SRSWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult CreateUser([FromBody] UserSignUpDTO user)
        {
            var result = _userRepository.SignUp(user);
            return Ok(result);
        }


        [HttpPost]
        [Route("SignIn")]
        [ProducesResponseType(200)]
        public IActionResult SignIn(string username, string password)
        {
            if (!_userRepository.SignIn(username, password))
            {
                return Unauthorized();
            }

            return Ok("It is true");
        }



    }
}
