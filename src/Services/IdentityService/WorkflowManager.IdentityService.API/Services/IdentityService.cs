using System;
using System.Collections.Generic;
using WorkflowManager.IdentityService.Infrastructure.Repositories;

namespace WorkflowManager.IdentityService.API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public IdentityService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public void RegisterUser(string Name, string Password)
        {
            var user = _userRepository.GetUser(Name);

            if (user != null)
            {
                throw new Exception($"User '{Name}' is already registred.");
            }

            var defaultRoles = new List<string> { "User" };

            _userRepository.AddUser(Name, Password, defaultRoles);
        }

        public string RenewToken(Guid userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new Exception($"User with given id: {userId} not exists.");
            }

            return _tokenService.GetToken(user);
        }

        public string SignIn(string Name, string Password)
        {
            var user = _userRepository.GetUser(Name, Password);
            if (user == null)
            {
                throw new Exception("Invalid Credencials");
            }

            return _tokenService.GetToken(user);

        }



    }
}
