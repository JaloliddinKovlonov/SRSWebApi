﻿using Microsoft.AspNetCore.Identity;
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
            string password = RandomStringGenerator();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
			string email = user.FirstName + "." +  user.LastName + "@final.edu.tr";

            User newUser = new User();
            var userName = user.FirstName + '.' + user.LastName;

            newUser.UserName = userName;
            newUser.RoleId = user.RoleId;
            newUser.Password = hashedPassword;
            newUser.Email = email;
            newUser.LastLogin = DateTime.Now;
            newUser.CreatedOn = DateTime.Now;
            newUser.ModifiedOn = DateTime.Now;
            newUser.IsDeleted = false;
            newUser.IsActive = true;

            _context.Add(newUser);

            if (Save())
            {
                newUser.Password = password;
                return newUser;
            }
            else
            {
                return new User();
            }
            
        }

        public User SignIn(string username, string password)
        {
            var user = GetUserByUsername(username);

			if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password) || !user.IsActive)
				return null;

			if (user == null) return null;

            user.LastLogin = DateTime.Now;

            _context.Users.Update(user);
            Save();
            return user;

        }
        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }



        string RandomStringGenerator()
        {
            Random res = new Random();

            String str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int size = 8;

            String ran = "";

            for (int i = 0; i < size; i++)
            {

                int x = res.Next(62);

                ran = ran + str[x];
            }

            return ran;
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
								.FirstOrDefault(t => t.UserId == userId && !t.IsRevoked);

			if (existingToken != null)
			{
				existingToken.Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
				existingToken.Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration.GetSection("JWT:RefreshTokenValidityInDays").Value));
				existingToken.Created = DateTime.UtcNow;
				existingToken.IsRevoked = false;
				existingToken.IsUsed = false;
				existingToken.LastLoggedInIP = ipAddress;

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
					IsRevoked = false,
					IsUsed = false,
					LastLoggedInIP = ipAddress
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

			if (user == null || !user.RefreshTokens.Any(t => t.Token == token && !t.IsUsed && !t.IsRevoked))
			{
				return null;
			}

			var oldToken = user.RefreshTokens.Single(t => t.Token == token);
			oldToken.IsUsed = true;
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
