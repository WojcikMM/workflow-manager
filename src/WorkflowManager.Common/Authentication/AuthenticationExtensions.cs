﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.Configuration;

namespace WorkflowManager.Common.Authentication
{
    public static class AuthenticationExtensions
    {
        public static void AddClientAuthentication(this IServiceCollection services, string configurationSectionName = "Authentication")
        {
            var options = services.GetOptions<AuthenticationConfigurationModel>(configurationSectionName);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(configuration =>
               {
                   configuration.Audience = options.Audience;
                   configuration.Authority = options.Authority;
                   configuration.MetadataAddress = $"{options.Authority}/.well-known/openid-configuration";
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