using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SRSWebApi.Data;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SRSWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SrsContext _context;
		private readonly IConfiguration _configuration;
		public UserRepository(IConfiguration configuration, SrsContext srsContext)
        {
            _context = srsContext;
			_configuration = configuration;
		}
        public User SignUp(UserSignUpDTO user)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            User newUser = new User();
            var userName = user.FirstName + '.' + user.LastName;

            newUser.UserName = userName;
            newUser.RoleId = user.RoleId;
            newUser.Password = hashedPassword;
            newUser.Email = user.Email;
            newUser.LastLogin = DateTime.Now;
            newUser.CreatedOn = DateTime.Now;
            newUser.ModifiedOn = DateTime.Now;
            newUser.IsDeleted = 0;
            newUser.IsActive = 1;

            _context.Add(newUser);

            if (Save())
            {
                return newUser;
            }
            else
            {
                return new User();
            }
            
        }

        public User SignIn(UserSignInDTO userSignInDTO)
        {
			var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == userSignInDTO.Email);

			if (user == null || !BCrypt.Net.BCrypt.Verify(userSignInDTO.Password, user.Password) || (user.IsActive == 0))
				return null;

			if (user == null) return null;

            user.LastLogin = DateTime.Now;

            _context.Users.Update(user);
            Save();
            return user;

        }
        public User GetUserByUsername(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

		public bool UserExists(string username)
		{
			return _context.Users.Any(u => u.UserName == username);
		}

		public bool UserExistsByName(string firstName, string lastName)
		{
			return _context.Users.Any(u => u.UserName == (char.ToUpper(firstName[0]) + firstName.Substring(1) + '.' + char.ToUpper(lastName[0]) + lastName.Substring(1)));
		}

		public RefreshToken GenerateOrUpdateRefreshToken(int userId, HttpRequest request)
		{
			var ipAddress = GetIpAddress(request);

			var existingToken = _context.RefreshTokens
								.FirstOrDefault(t => t.UserId == userId && (t.IsRevoked == 0));

			if (existingToken != null)
			{
				existingToken.Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
				existingToken.Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration.GetSection("JWT:RefreshTokenValidityInDays").Value));
				existingToken.Created = DateTime.UtcNow;
				existingToken.IsRevoked = 0;
				existingToken.IsUsed = 0;
				existingToken.LastLoggedInIp = ipAddress;

				_context.RefreshTokens.Update(existingToken);
			}
			else
			{
				var newRefreshToken = new RefreshToken
				{
					UserId = userId,
					Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
					Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration.GetSection("JWT:RefreshTokenValidityInDays").Value)),
					Created = DateTime.UtcNow,
					IsRevoked = 0,
					IsUsed = 0,
					LastLoggedInIp = ipAddress
				};

				_context.RefreshTokens.Add(newRefreshToken);
				existingToken = newRefreshToken;
			}
			_context.SaveChanges();
			return existingToken;
		}

		public ActionResult<object> RefreshToken(string token, HttpRequest request)
		{
			var user = _context.Users.Include(u => u.RefreshTokens).Include(u => u.Role).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

			if (user == null || !user.RefreshTokens.Any(t => t.Token == token && (t.IsUsed == 1) && (t.IsRevoked == 1)))
			{
				return null;
			}

			var oldToken = user.RefreshTokens.Single(t => t.Token == token);
			oldToken.IsUsed = 1;
			_context.RefreshTokens.Update(oldToken);

			var updatedRefreshToken = GenerateOrUpdateRefreshToken(user.UserId, request);
			_context.SaveChanges();

			var newJwtToken = CreateToken(user);
			return new { jwt = newJwtToken, refreshToken = updatedRefreshToken.Token };
		}

		public string GetIpAddress(HttpRequest request)
		{
			if (request.Headers.ContainsKey("X-Forwarded-For"))
				return request.Headers["X-Forwarded-For"];
			else
				return request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
		}

		public string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "User")
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddMinutes(int.Parse(_configuration.GetSection("JWT:TokenValidityInMintues").Value)),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
