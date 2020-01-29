using System;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using WorkflowManager.IdentityService.Infrastructure.Context;
using System.Linq;
using System.Collections.Generic;

namespace WorkflowManager.IdentityService.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettingsModel _options;

        public TokenService(IOptions<JwtSettingsModel> options)
        {
            _options = options.Value;
        }

        public string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = new SymmetricSecurityKey(_options.SecretBytes);

            var userRolesClaims = user.UserRoles?
                .Select(m => new Claim(ClaimTypes.Role, m.Role.Name))
                .ToList() ?? new List<Claim>();

            var tokenClaims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Name, user.Id.ToString())
            };

            tokenClaims.AddRange(userRolesClaims);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(tokenClaims),
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                Expires = DateTime.UtcNow.AddSeconds(_options.ExpireSeconds),
                SigningCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtTokenString = tokenHandler.WriteToken(token);
            return jwtTokenString;
        }


    }
}
