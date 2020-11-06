using IdentityServerAspNetIdentity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.ApplicationInitializer;
using WorkflowManager.Common.Authentication;
using WorkflowManager.Common.Configuration;
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
            services.AddCorsAbility();
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
            ServiceConfiguration.InjectCommonMiddlewares(app, env);
            app.UseStaticFiles();
            app.UseIdentityServer();
        }
    }
}