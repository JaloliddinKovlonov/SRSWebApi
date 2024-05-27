using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;
using SRSWebApi.Repository;
using System.Security.Claims;

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

		[HttpPost("SignUp")]
        [ProducesResponseType(200)]
        public IActionResult SignUp([FromBody] UserSignUpDTO user)
        {
            if (_userRepository.UserExistsByName(user.FirstName, user.LastName))
            {
				return BadRequest("User with the same username already exists");
			}

			var identity = HttpContext.User.Identity as ClaimsIdentity;
			var userIdClaim = identity?.FindFirst(ClaimTypes.NameIdentifier);


			var result = _userRepository.SignUp(user);
			if (user == null) return BadRequest("Registration failed.");
			return Ok(result);
        }


		[HttpPost("SignIn")]
		[ProducesResponseType(200)]
		public IActionResult SignIn([FromBody] UserSignInDTO userSignInDTO)
		{
			var user = _userRepository.SignIn(userSignInDTO);

			if (user == null)
				return Unauthorized("Invalid credentials or user is inactive.");

			var refreshToken = _userRepository.GenerateOrUpdateRefreshToken(user.UserId, HttpContext.Request);
			var jwtToken = _userRepository.CreateToken(user);

			return Ok(new
			{
				jwt = jwtToken,
				refreshToken = refreshToken.Token,
				user = new
				{
					user.UserId,
					user.UserName,
					user.Email,
					RoleName = user.Role.RoleName,
					user.LastLogin,
					user.CreatedOn
				}
			});
		}



		[HttpPost("refresh-token")]
		public ActionResult<object> RefreshToken(RefreshTokenDTO request)
		{
			var result = _userRepository.RefreshToken(request.Token, HttpContext.Request);

			if (result == null)
			{
				return BadRequest("Invalid refresh token.");
			}

			return Ok(result);
		}


	}
}
