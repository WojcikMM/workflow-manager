using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WorkflowManager.Common.Configuration;
using WorkflowManager.Common.ReadModelStore;
using WorkflowManager.Common.Swagger;
using WorkflowManager.IdentityService.API.Services;
using WorkflowManager.IdentityService.Infrastructure.Context;
using WorkflowManager.IdentityService.Infrastructure.Repositories;

namespace WorkflowManager.IdentityService.API
{
    public class JwtSettingsModel
    {
        public string Secret { get; set; } = "SuperSecret2000!";
        public string Issuer { get; set; } = "http://localhost:5000";
        public string Audience { get; set; } = "http://localhost:5000";
        public int ExpireSeconds { get; set; } = 60;

        public byte[] SecretBytes
        {
            get
            {
                return Encoding.UTF8.GetBytes(Secret);
            }
        }

    }


    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddServiceSwaggerUI();
            var jwtSettings = Configuration.GetSection("Jwt");
            services.Configure<JwtSettingsModel>(jwtSettings);
            services.AddReadModelStore<IdentityDatabaseContext>("MsSqlDatabase");
            services.AddTransient<IIdentityService, Services.IdentityService>();
            services.AddTransient<ITokenService, Services.TokenService>();
            services.AddTransient<IUserRepository, UserRepository>();
            AddJwtAuthentication(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseServiceSwaggerUI();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddJwtAuthentication(IServiceCollection services)
        {
            var jwtSettings = services.GetOptions<JwtSettingsModel>("jwt");

            var secret = new SymmetricSecurityKey(jwtSettings.SecretBytes);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // TODO: Change in future
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = secret,
                    ValidateIssuer = false,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = false,
                    ValidAudience = jwtSettings.Audience,
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
            });
        }
    }
}
