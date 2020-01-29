using WorkflowManager.IdentityService.Infrastructure.Context;

namespace WorkflowManager.IdentityService.API.Services
{
    public interface ITokenService
    {
        string GetToken(User user);
    }
}