using Microsoft.AspNetCore.Mvc;
using SRSWebApi.DTO;
using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
    public interface IUserRepository
    {
        User SignUp(UserSignUpDTO user);
		User SignIn(UserSignInDTO user);
        User GetUserByUsername(string UserName);
		bool UserExists(string username);
		bool UserExistsByName(string firstName, string lastName);
		RefreshToken GenerateOrUpdateRefreshToken(int userId, HttpRequest request);
		ActionResult<object> RefreshToken(string token, HttpRequest request);
		string GetIpAddress(HttpRequest request);
		string CreateToken(User user);
	}
}
