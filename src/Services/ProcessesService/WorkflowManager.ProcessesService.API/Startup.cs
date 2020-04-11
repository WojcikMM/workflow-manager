using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.Authentication;
using WorkflowManager.Common.CQRSHandlers;
using WorkflowManager.Common.EventStore;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.Common.Messages.Events.Processes.Complete;
using WorkflowManager.Common.Messages.Events.Processes.Rejected;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.Common.ReadModelStore;
using WorkflowManager.Common.Swagger;
using WorkflowManager.ProcessesService.ReadModel;
using WorkflowManager.ProcessesService.ReadModel.ReadDatabase;
using WorkflowManager.ProcessesService.Core.CommandHandlers;
using WorkflowManager.ProcessesService.Core.EventHandlers;
using WorkflowManager.Common.ApplicationInitializer;
using WorkflowManager.Common.Cors;

namespace WorkflowManager.ProcessesService.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
            services.AddRabbitMq();
            services.AddEventStore(NEventStore.Logging.LogLevel.Info, "MsSqlDatabase");
            services.AddServiceSwaggerUI();
            services.AddClientAuthentication();

            services.AddReadModelStore<ProcessesContext>("MsSqlDatabase");
            services.AddReadModelRepository<ProcessModel, ProcessReadModelRepository>();

            services.AddCommandHandler<CreateProcessCommand, CreateProcessCommandHandler>()
                    .AddCommandHandler<UpdateProcessCommand, UpdateProcessCommandHandler>()
                    .AddCommandHandler<RemoveProcessCommand, RemoveProcessCommandHandler>()

                    .AddEventHandler<ProcessCreatedEvent, ProcessCreatedEventHandler>()
                    .AddEventHandler<ProcessNameUpdatedEvent, ProcessNameUpdatedEventHandler>()
                    .AddEventHandler<ProcessRemovedEvent, ProcessRemovedEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServiceConfiguration.InjectCommonMiddlewares(app, env);
            //  app.RegisterCorsMiddleware();
            app.UseCors("CORSPolicy");
            app.UseRabbitMq()
                .SubscribeCommand<CreateProcessCommand, ProcessCreateRejectedEvent>()
                .SubscribeCommand<UpdateProcessCommand, ProcessUpdateRejectedEvent>()
                .SubscribeCommand<RemoveProcessCommand, ProcessRemoveRejectedEvent>()

                .SubscribeEvent<ProcessCreatedEvent, ProcessCreateRejectedEvent, ProcessCreateCompleteEvent>()
                .SubscribeEvent<ProcessNameUpdatedEvent, ProcessUpdateRejectedEvent, ProcessUpdateCompleteEvent>()
                .SubscribeEvent<ProcessRemovedEvent, ProcessRemoveRejectedEvent, ProcessRemoveCompleteEvent>();
        }
    }
}
