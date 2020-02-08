using RestEase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.Gateway.API.Services;
using WorkflowManager.Common.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace WorkflowManager.Gateway.API
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
                            },
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
                    In = Microsoft.OpenApi.Models.ParameterLocation.Query
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

            // Gateway configuration
            var gatewayServicesUrls = services.GetOptions<GatewayServicesConfigurationModel>("Services", failIfNotExists: true);


            services.AddTransient(service =>
                        RestClient.For<IProcessesService>(gatewayServicesUrls.ProcessServiceUrl));

            services.AddTransient(service =>
                        RestClient.For<IStatusesService>(gatewayServicesUrls.StatusServiceUrl));

            services.AddTransient(service =>
                        RestClient.For<IOperationsService>(gatewayServicesUrls.OperationsServiceUrl));

            services.AddRabbitMq();
            services.AddCors();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddOpenIdConnect(options =>
            {
                options.MetadataAddress = "http://localhost:5000/.well-known/openid-configuration";
                options.RequireHttpsMetadata = false;
                options.RequireHttpsMetadata = false;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Authority = "http://localhost:5000/";
                options.ClientId = "swagger";
                options.ResponseType = "id_token";
                options.SaveTokens = true;

                options.Scope.Add("api1");
            });


            //    .AddJwtBearer(o =>
            //{
            //    o.Authority = "http://localhost:5000";
            //    o.Audience = "api1";
            //    o.RequireHttpsMetadata = false;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseHttpsRedirection();
            //app.UseAllForwardedHeaders();
            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_appServiceName} ({_appServiceVersion})");
                cfg.RoutePrefix = string.Empty;
            });
            app.UseRabbitMq();
            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
