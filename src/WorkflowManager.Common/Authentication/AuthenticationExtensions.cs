using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using WorkflowManager.Common.Configuration;

namespace WorkflowManager.Common.Authentication
{
    public static class AuthenticationExtensions
    {
        public static void AddClientAuthentication(this IServiceCollection services, string configurationSectionName = "Authentication")
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
                   var options = services.GetOptions<AuthenticationConfigurationModel>(configurationSectionName);
                   configuration.Audience = options.Audience;
                   configuration.Authority = options.Authority;
                   configuration.MetadataAddress = options.MetadataAddress;
                   configuration.RequireHttpsMetadata = false;
                   configuration.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       ValidateAudience = true,
                       ValidAudience = options.Audience,
                       ValidateIssuer = true,
                       ValidIssuer = options.Authority
                   };
               }
               );

        }
    }
}
