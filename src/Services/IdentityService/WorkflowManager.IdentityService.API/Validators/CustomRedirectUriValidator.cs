using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace WorkflowManager.IdentityService.API.Validators
{
    public class CustomRedirectUriValidator : IRedirectUriValidator
    {
        public Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
        {
            // TODO: Change after development
            return Task.FromResult(ValidateUri(requestedUri));
        }

        public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
        {
            // TODO: Change after development
            return Task.FromResult(ValidateUri(requestedUri));
        }

        private bool ValidateUri(string requestedUri)
        {
            var isFromAzure = Regex.IsMatch(requestedUri, @"workflow-manager.*\.azurewebsites.net");
            var isLocalhost = requestedUri.Contains("localhost:");

            return isFromAzure || isLocalhost;
        }
    }
}
