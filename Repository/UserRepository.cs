using Microsoft.EntityFrameworkCore.Query.Internal;
using SRSWebApi.DTO;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace SRSWebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SrsContext _context;
        public UserRepository(SrsContext srsContext)
        {
            _context = srsContext;
        }
        public User SignUp(UserSignUpDTO user)
        {
            string salt = GenerateSalt();
            string password = RandomStringGenerator();
            string hashedPassword = HashPassword(password, salt);
            string email = user.FirstName + "." +  user.LastName + "@final.edu.tr";

            User newUser = new User();
            var userName = user.FirstName + '.' + user.LastName;

            newUser.UserName = userName;
            newUser.RoleId = user.RoleId;
            newUser.Password = hashedPassword;
            newUser.Salt = salt;
            newUser.Email = email;
            newUser.LastLogin = DateTime.Now;
            newUser.CreatedOn = DateTime.Now;
            newUser.ModifiedOn = DateTime.Now;
            newUser.IsDeleted = false;
            newUser.IsActive = false;

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

        public bool SignIn(string username, string password)
        {
            var user = GetUserByUsername(username);
            var salt = user.Salt;
            var hashedpassword = HashPassword(password, salt);
            if (user == null) return false;

            return user.Password == hashedpassword;

        }
        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }

        protected string GenerateSalt()
        {
            const string chars = "jIW5ReMdi9asdiC5IOH7uwZqweqwKML4YFrq15fsdfsVywBsHmiI";
            var random = new Random();
            var saltChars = new char[16];
            for (int i = 0; i < saltChars.Length; i++)
            {
                saltChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(saltChars);
        }


        protected string HashPassword(string password, string salt)
        {
            string combinedPassword = password + salt;

            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(combinedPassword);

                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("x2"));
                }

                return result.ToString();
            }
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
    }
}
