using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WorkflowManager.IdentityService.Infrastructure.Context;

namespace WorkflowManager.IdentityService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDatabaseContext _databaseContext;

        public UserRepository(IdentityDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        public IEnumerable<User> GetUsers() => _databaseContext.Users.ToList();

        public User GetUser(Guid id) => _databaseContext.Users.Find(id);

        public User GetUser(string Name, string Password)
        {
            var passwordHash = _computeHash(Password);
            return _databaseContext.Users
                .FirstOrDefault(m => m.Name == Name && m.Password == passwordHash);
        }
        public User GetUser(string Name) =>
         _databaseContext.Users.FirstOrDefault(m => m.Name == Name);


        public void AddUser(Guid Id, string Name, string Password, IEnumerable<string> roles)
        {

            var user = new User
            {
                Id = Id,
                Name = Name,
                Password = _computeHash(Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastLoginAt = null
            };

            _databaseContext.Users.Add(user);
            _databaseContext.SaveChanges();
        }


        private string _computeHash(string password)
        {
            using (var sha256 = new SHA256Managed())
            {
                var hashBuilder = new StringBuilder();
                var bytesPassword = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytesPassword);
                foreach (byte x in hash)
                    hashBuilder.Append($"{x:x2}");

                return hashBuilder.ToString();
            }
        }

        public void AddUser(string Name, string Password, IEnumerable<string> roles)
        {
            AddUser(Guid.NewGuid(), Name, Password, roles);
        }


    }
}
