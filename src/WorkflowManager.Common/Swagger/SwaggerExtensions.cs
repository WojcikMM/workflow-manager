using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.Configuration;

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
