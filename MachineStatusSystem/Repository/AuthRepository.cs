using MachineStatusSystem.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineStatusSystem.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserContext _context;
        private readonly IConfiguration _config;
        public AuthRepository(UserContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        
        public string Login(string userName, string password)
        {
            var user = _context.LoginUser.FirstOrDefault(x => x.Username.ToLower().Equals(userName.ToLower()));
            if (user == null)
            {
                return "0";
            }
            else if (VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                 return user.Id.ToString();
            }
            else
            {
                return "0";
            }
        }

        public int Register(LoginUser user, string password)
        {
            if (UserExists(user.Username))
            {
                return 0;
              
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.LoginUser.Add(user);
            _context.SaveChanges();

            return user.Id;
        }

        public bool UserExists(string userName)
        {
            if (_context.LoginUser.Any(x => x.Username.ToLower() == userName.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                       
                    }
                }
                return true;
            }
        }  
    }
}