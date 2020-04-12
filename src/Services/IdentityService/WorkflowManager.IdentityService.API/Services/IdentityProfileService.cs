using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace WorkflowManager.IdentityService.API.Services
{
    public class IdentityProfileService : IProfileService
    {
        private UserManager<IdentityUser> _userManager;

        public IdentityProfileService(UserManager<IdentityUser> userManager) =>
            _userManager = userManager;

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {

            var user = await _userManager.GetUserAsync(context.Subject);
            var userRoles = await _userManager.GetRolesAsync(user);


            var userRoleClaims = userRoles
                .Select(roleName => new Claim(ClaimTypes.Role, roleName))
                .ToList();

            context.IssuedClaims.AddRange(userRoleClaims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }
    }
}
