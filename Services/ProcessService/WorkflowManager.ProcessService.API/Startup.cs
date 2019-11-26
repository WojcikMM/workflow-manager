using CQRS.Template.Domain.CommandHandlers;
using CQRS.Template.Domain.EventHandlers;
using CQRS.Template.Domain.Storage;
using CQRS.Template.ReadModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowManager.Common.EventStore;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.ProcessService.API.Middlewares;
using WorkflowManager.ProcessService.Infrastructure.Storage;
using WorkflowManager.ProcessService.ReadModel;
using WorkflowManager.ProcessService.ReadModel.ReadDatabase;
using WorkflowManager.ProductService.Core.CommandHandlers;
using WorkflowManager.ProductService.Core.EventHandlers;

namespace WorkflowManager.ProductService.API
{
    public class Startup
    {
        private const string _appServiceName = "Product API Service";
        private const string _appServiceVersion = "v1";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


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

            services.AddRabbitMq();
            services.AddEventStore();
            //services.AddEntityFrameworkSqlServer();
            var readDbConnectionString = Configuration.GetConnectionString("ServiceReadDatabase");
            services.AddDbContext<ProcessesContext>(options => options.UseSqlServer(readDbConnectionString),
                optionsLifetime: ServiceLifetime.Transient, contextLifetime: ServiceLifetime.Transient);
            //services.AddSingleton<ICommandBus, CommandBus>();
            //services.AddSingleton<IEventBus, InMemoryEventBus>();
            //
            //services.AddSingleton<IEventBus, RabbitMqEventBus>();
            //services.AddSingleton<IEventStorage, InMemoryEventStorage>();

            services.AddSingleton(typeof(IRepository<>), typeof(AggregateRespository<>));

            services.AddTransient<IReadModelRepository<ProcessModel>, ProcessReadModelRepository>();

            //services.AddSingleton<IReadModelRepository<ProcessReadModel>, InMemoryProcessReadModelRepository>();

            RegisterCommandHandlers(services);
            RegisterEventHandlers(services);
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
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_appServiceName} ({_appServiceVersion})");
                cfg.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseRabbitMq()
                .SubscribeCommand<CreateProcessCommand>()
                .SubscribeCommand<UpdateProcessCommand>()
                .SubscribeCommand<RemoveProcessCommand>()

                .SubscribeEvent<ProcessCreatedEvent>()
                .SubscribeEvent<ProcessNameUpdatedEvent>()
                .SubscribeEvent<ProcessRemovedEvent>();

            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        /// <summary>
        /// Method to register event handlers
        /// </summary>
        /// <param name="services"></param>
        private void RegisterEventHandlers(IServiceCollection services)
        {
            services.AddTransient<IEventHandler<ProcessCreatedEvent>, ProcessCreatedEventHandler>();
            services.AddTransient<IEventHandler<ProcessNameUpdatedEvent>, ProcessNameUpdatedEventHandler>();
            services.AddTransient<IEventHandler<ProcessRemovedEvent>, ProcessRemovedEventHandler>();
        }

        /// <summary>
        /// Method to register command handlers
        /// </summary>
        /// <param name="services"></param>
        private void RegisterCommandHandlers(IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<CreateProcessCommand>, CreateProcessCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateProcessCommand>, UpdateProcessCommandHandler>();
            services.AddTransient<ICommandHandler<RemoveProcessCommand>, RemoveProcessCommandHandler>();
        }
    }
}
