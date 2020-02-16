using IdentityServerAspNetIdentity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.ApplicationInitializer;
using WorkflowManager.Common.Authentication;
using WorkflowManager.Common.ReadModelStore;
using WorkflowManager.Common.Swagger;

namespace IdentityServerAspNetIdentity
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddClientAuthentication();
            services.AddReadModelStore<ApplicationDbContext>("MsSqlDatabase");

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                //  .AddSigningCredential("tempkey")
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryClients(Config.Clients)
                .AddTestUsers(Config.TestUsers)
                .AddAspNetIdentity<IdentityUser>();

            services.AddServiceSwaggerUI();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServiceConfiguration.InjectCommonMiddlewares(app, env, false);

            app.UseStaticFiles();
            app.UseIdentityServer();
        }
    }
}