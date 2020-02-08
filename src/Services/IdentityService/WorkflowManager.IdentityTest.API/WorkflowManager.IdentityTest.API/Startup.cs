using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace WorkflowManager.IdentityTest.API
{

    public class Startup
    {
        private const string _appServiceName = "API Gateway Service";
        private const string _appServiceVersion = "v1";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc(_appServiceVersion,
                     new Microsoft.OpenApi.Models.OpenApiInfo
                     {
                         Title = _appServiceName,
                         Version = _appServiceVersion
                     });
                cfg.AddSecurityDefinition("spa", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Name = "swagger",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2,
                    OpenIdConnectUrl = new System.Uri("http://localhost:5000/.well-known/openid-configuration"),
                    Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows()
                    {
                        Implicit = new Microsoft.OpenApi.Models.OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new System.Uri("http://localhost:5000/connect/authorize"),
                            TokenUrl = new System.Uri("http://localhost:5000/connect/token"),
                            Scopes = new Dictionary<string, string> {
                                { "api1", "Demo API - full access" },
                            }
                        },
                        //ClientCredentials = new Microsoft.OpenApi.Models.OpenApiOAuthFlow()
                        //{
                        //    AuthorizationUrl = new System.Uri("http://localhost:5000/connect/authorize"),
                        //    TokenUrl = new System.Uri("http://localhost:5000/connect/token"),
                        //    Scopes = new Dictionary<string, string> {
                        //        { "api1", "Demo API - full access" },
                        //    }
                        //}
                    },
                    // In = Microsoft.OpenApi.Models.ParameterLocation.Query
                });

                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "spa" }
            },
            new[] { "readAccess", "writeAccess" }
        }
    });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.Audience = "api1";
                    options.Authority = "http://localhost:5000";
                    options.MetadataAddress = "http://localhost:5000/.well-known/openid-configuration";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = "api1"
                    };
                }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_appServiceName} ({_appServiceVersion})");
                cfg.RoutePrefix = string.Empty;
            });


            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
