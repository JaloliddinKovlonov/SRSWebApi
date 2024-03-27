using SRSWebApi.DTO;
using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
    public interface IUserRepository
    {
        User SignUp(UserSignUpDTO user);
        bool SignIn(string UserName, string Password);
        User GetUserByUsername(string UserName);


    }
}
