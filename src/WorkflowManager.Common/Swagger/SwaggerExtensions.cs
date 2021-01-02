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

            app.UseSwagger(cfg =>
            {
                cfg.SerializeAsV2 = true;
            });
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
            var identityUrl = services.GetIdentityUrl();

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
                    OpenIdConnectUrl = new System.Uri($"{identityUrl}/.well-known/openid-configuration"),
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new System.Uri($"{identityUrl}/connect/authorize"),
                            TokenUrl = new System.Uri($"{identityUrl}/connect/token"),
                            Scopes = options.Scopes,

                        }
                    }
                });
                cfg.SwaggerDoc(serviceInfomations.ServiceVersion,
                    new OpenApiInfo
                    {
                        Title = serviceInfomations.ServiceName,
                        Version = serviceInfomations.ServiceVersion
                    });
            });

        }
    }
}
