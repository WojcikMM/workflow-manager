using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WorkflowManager.Common.Configuration
{
    public static class CorsExtensions
    {
        private static string _policyName = "CorsPolicy";

        public static void AddCorsAbility(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_policyName, builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        }

        public static IApplicationBuilder RegisterCorsMiddleware(this IApplicationBuilder app)
        {
            app.UseCors(_policyName);
            return app;
        }
    }
}
