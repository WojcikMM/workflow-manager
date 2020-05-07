using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using WorkflowManager.Common.Configuration;

namespace WorkflowManager.Common.Authentication
{
    public static class AuthenticationExtensions
    {
        public static void AddClientAuthentication(this IServiceCollection services)
        {
            // TODO: This is only for development mode
            IdentityModelEventSource.ShowPII = true;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(configuration =>
               {
                   var IssuerUrl = services.GetIdentityUrl();
                   var identityAudience = services.GetValue<string>("IdentityAudience");
                   var identityInternalUrl = services.GetValue<string>("IdentityInternalUrl") ?? IssuerUrl;

                   configuration.Audience = identityAudience;
                   configuration.Authority = IssuerUrl;
                   configuration.MetadataAddress = $"{IssuerUrl}/.well-known/openid-configuration";
                   configuration.RequireHttpsMetadata = false;
                   configuration.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       ValidateAudience = true,
                       ValidAudience = identityAudience,
                       ValidateIssuer = true,
                       ValidIssuer = IssuerUrl
                   };
               }
               );

        }
    }
}
