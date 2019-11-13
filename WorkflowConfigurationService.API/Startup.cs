using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowConfiguration.Infrastructure.Commands;
using WorkflowConfigurationService.Domain.Bus;
using WorkflowConfigurationService.Domain.CommandHandlers;
using WorkflowConfigurationService.Domain.CommandHandlers.Processes;
using WorkflowConfigurationService.Domain.Storage;
using WorkflowConfigurationService.Infrastructure.Bus;
using WorkflowConfigurationService.Infrastructure.Storage;

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
            

            //services.AddSingleton(AutoMapperConfig.Initialize());
            //services.AddScoped<IProcessService, ProcessService>();
            //services.AddScoped<IStatusService, StatusService>();
            //services.AddScoped<ITransactionService, TransactionService>();


            //services.AddScoped<IProcessRepository, ProcessRepository>();
            //services.AddScoped<IStatusRepository, StatusRepository>();
            //services.AddScoped<ITransactionRepository, TransactionRepository>();
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
