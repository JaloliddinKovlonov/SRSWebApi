using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
    public interface IJWTtoken
    {
        public bool GenerateToken(User user);
        
    }
}
