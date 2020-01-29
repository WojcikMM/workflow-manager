using System;
using System.Collections.Generic;
using WorkflowManager.IdentityService.Infrastructure.Context;

namespace WorkflowManager.IdentityService.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        void AddUser(string Name, string Password, IEnumerable<string> roles);
        User GetUser(Guid Id);

        User GetUser(string Name);
        User GetUser(string Name, string Password);
        IEnumerable<User> GetUsers();
    }
}