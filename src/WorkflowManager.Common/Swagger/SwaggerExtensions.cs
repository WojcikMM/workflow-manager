using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.Configuration;
using Microsoft.OpenApi.Models;
using WorkflowManager.Common.ApplicationInitializer;

namespace WorkflowManager.Common.Swagger
{
    public static class SwaggerExtensions
    {
        public static IApplicationBuilder UseServiceSwaggerUI(this IApplicationBuilder app, string configurationSectionName = "Swagger")
        {
            SwaggerConfigurationModel options = app.ApplicationServices
                                        .GetService<IConfiguration>()
                                        .GetOptions<SwaggerConfigurationModel>(configurationSectionName);

            var serviceInfomations = ServiceConfiguration.GetServiceInformations();

            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json",
                    serviceInfomations.ServiceNameWithVersion);
                cfg.RoutePrefix = string.Empty;
            });

            return app;
        }


        public static void AddServiceSwaggerUI(this IServiceCollection services, string configurationSectionName = "Swagger")
        {

            SwaggerConfigurationModel options = services.GetOptions<SwaggerConfigurationModel>(configurationSectionName);
            var serviceInfomations = ServiceConfiguration.GetServiceInformations();

            services.AddSwaggerGen(cfg =>
            {
                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement{
                        {
                            new OpenApiSecurityScheme {
                                Reference = new OpenApiReference()
                                {
                                    Id = "OpenId",
                                    Type = ReferenceType.SecurityScheme
                                }                               
                            }, new[] { "readAccess" }
                        }
                });
                cfg.AddSecurityDefinition("OpenId", new OpenApiSecurityScheme
                {
                    Name = "swagger",
                    Type = SecuritySchemeType.OAuth2,
                    OpenIdConnectUrl = new System.Uri($"{options.IdentityServiceUrl}/.well-known/openid-configuration"),
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new System.Uri($"{options.IdentityServiceUrl}/connect/authorize"),
                            TokenUrl = new System.Uri($"{options.IdentityServiceUrl}/connect/token"),
                            Scopes = options.Scopes
                        },
                    }
                });
                cfg.SwaggerDoc(serviceInfomations.ServiceNameWithVersion,
                    new OpenApiInfo
                    {
                        Title = serviceInfomations.ServiceName,
                        Version = serviceInfomations.ServiceVersion
                    });
            });

        }
    }
}
