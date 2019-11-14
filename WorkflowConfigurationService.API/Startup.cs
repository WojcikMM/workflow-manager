using CQRS.Template.Domain.Bus;
using CQRS.Template.Domain.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using CQRS.Template.Domain.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using WorkflowConfigurationService.Infrastructure.Bus;
using WorkflowConfigurationService.Core.Processes.Commands;
using WorkflowConfigurationService.Infrastructure.Storage;
using WorkflowConfigurationService.Core.Processes.CommandHandlers;

namespace WorkflowConfigurationService.API
{
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

            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Workflow Configuration Service",
                        Version = "v1"
                    });
            });

            services.AddSingleton<ICommandBus, CommandBus>();
            services.AddSingleton<IEventBus, InMemoryEventBus>();
            services.AddSingleton<IEventStorage, InMemoryEventStorage>();

            services.AddSingleton(typeof(IRepository<>), typeof(InMemoryAggregateRepository<>));

            services.AddTransient<ICommandHandler<CreateProcessCommand>, CreateProcessCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateProcessCommand>, UpdateProcessCommandHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "Workflow Configuration Service (v1)");
                cfg.RoutePrefix = string.Empty;
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
