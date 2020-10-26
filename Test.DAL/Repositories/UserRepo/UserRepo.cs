using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.DOM;
using Test.DOM.Models;

namespace Test.DAL.Repositories.UserRepo
{
    public class UserRepo : IUserRepo
    {
        private readonly TestContext _context;
        public UserRepo(TestContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users
                .Where(u => u.EmployeeNo == username)
                .SingleOrDefault();

            if (user != null)
                return user;
            return null;
        }

        public bool Create(User user, string password)
        {
            if (user is null || string.IsNullOrEmpty(password)) return false;
            if (_context.Users.Any(u => u.EmployeeNo == user.EmployeeNo)) return false;

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            //user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            try
            {
                _context.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }

        private static void CreatePasswordHash(string pw, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (pw == null)
                throw new ArgumentException("password");
            if (string.IsNullOrWhiteSpace(pw))
                throw new ArgumentException("Value cannot be empty or whitespace-only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pw));
            }
        }

        private bool VerifyPasswordHash(string pw, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrEmpty(pw))
                throw new ArgumentNullException(nameof(pw));
            if (string.IsNullOrWhiteSpace(pw))
                throw new ArgumentException("Value cannot be empty or whitespace-only string.", "password");
            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordSalt");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pw));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
