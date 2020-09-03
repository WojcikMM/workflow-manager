using IdentityServer4;
using IdentityServerAspNetIdentity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.ApplicationInitializer;
using WorkflowManager.Common.Authentication;
using WorkflowManager.Common.ReadModelStore;
using WorkflowManager.Common.Swagger;
using WorkflowManager.IdentityService.API.Services;
using WorkflowManager.IdentityService.API.Validators;

namespace IdentityServerAspNetIdentity
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
            ServiceConfiguration.InjectCommonServices(services, false);
            services.AddClientAuthentication();
            services.AddReadModelStore<ApplicationDbContext>("MsSqlDatabase");

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(setup =>
            {
                setup.UserInteraction.ErrorUrl = "/error";
            })
                //  .AddSigningCredential("tempkey")
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<IdentityUser>()
                .AddProfileService<IdentityProfileService>()
                .AddRedirectUriValidator<CustomRedirectUriValidator>();

            services.ConfigureNonBreakingSameSiteCookies();

            services.AddServiceSwaggerUI();

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCookiePolicy();
            app.UseCors("CorsPolicy");
            ServiceConfiguration.InjectCommonMiddlewares(app, env);
            app.UseStaticFiles();
            app.UseIdentityServer();
        }
    }
}