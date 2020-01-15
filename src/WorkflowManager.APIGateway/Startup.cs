using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestEase;
using WorkflowManager.APIGateway;
using WorkflowManager.Common.RabbitMq;
using WorkflowManagerGateway.Services;
using WorkflowManager.Common.Configuration;

namespace WorkflowManagerGateway
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
            });

            // Gateway configuration
            var gatewayServicesUrls = services.GetOptions<GatewayServicesConfigurationModel>("Services", failIfNotExists: true);


            services.AddTransient(service =>
                        RestClient.For<IProcessesService>(gatewayServicesUrls.ProcessServiceUrl));

            services.AddTransient(service =>
                        RestClient.For<IStatusesService>(gatewayServicesUrls.StatusServiceUrl));

            services.AddRabbitMq();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
