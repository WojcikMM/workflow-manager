using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace WorkflowManager.Common.Swagger
{
    public static class SwaggerExtensions
    {
        private static readonly string _swaggerOptionsName = "Swagger";
        public static IApplicationBuilder UseServiceSwaggerUI(this IApplicationBuilder app)
        {
            SwaggerOptions options = app.ApplicationServices
                                        .GetService<IConfiguration>()
                                        .GetOptions<SwaggerOptions>(_swaggerOptionsName);

            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.ServiceName} ({options.Version})");
                cfg.RoutePrefix = string.Empty;
            });

            return app;
        }


        public static void AddServiceSwaggerUI(this IServiceCollection services)
        {

            SwaggerOptions options;
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                options = serviceProvider.GetService<IConfiguration>()
                                         .GetOptions<SwaggerOptions>(_swaggerOptionsName);
            }

            services.AddSwaggerGen(cfg =>
            {
                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement{
                        {
                            new OpenApiSecurityScheme {
                                Reference = new OpenApiReference()
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }                               
                            }, new[] { "readAccess" }
                        }
                });
                cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                cfg.SwaggerDoc(options.Version,
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = options.ServiceName,
                        Version = options.Version
                    });
            });

        }
    }
}
