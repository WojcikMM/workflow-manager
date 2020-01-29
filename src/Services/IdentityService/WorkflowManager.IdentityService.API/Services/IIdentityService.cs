using System;

namespace WorkflowManager.IdentityService.API.Services
{
    public interface IIdentityService
    {
        void RegisterUser(string Name, string Password);
        string SignIn(string Name, string Password);
        string RenewToken(Guid userId);
    }
}