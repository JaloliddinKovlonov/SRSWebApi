using SRSWebApi.Interfaces;
using SRSWebApi.Models;

namespace SRSWebApi.Repository
{
    public class JWTtoken : IJWTtoken
    {
        public bool GenerateToken(User user)
        {
            return true;
        }
    }
}
