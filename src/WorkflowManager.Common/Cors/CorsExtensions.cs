using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WorkflowManager.Common.Cors
{
    public static class CorsExtensions
    {
        private static string policyName = "CorsPolicy";

        public static void AddCorsAbility(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName, builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        }

        public static IApplicationBuilder RegisterCorsMiddleware(this IApplicationBuilder app)
        {
            app.UseCors(policyName);
            return app;
        }
    }
}
