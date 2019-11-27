using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestEase;
using WorkflowManager.Common.RabbitMq;
using WorkflowManagerGateway.Services;

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
            });
            services.AddTransient<IProcessesService>(service =>
                        RestClient.For<IProcessesService>("http://localhost:5001/api"));
            services.AddRabbitMq();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
