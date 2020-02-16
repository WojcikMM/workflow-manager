using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.Swagger;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.Common.EventStore;
using WorkflowManager.Common.CQRSHandlers;
using WorkflowManager.Common.ReadModelStore;
using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.Common.Messages.Events.Statuses;
using WorkflowManager.StatusesService.ReadModel;
using WorkflowManager.StatusesService.Core.CommandHandlers;
using WorkflowManager.StatusesService.ReadModel.ReadDatabase;
using WorkflowManager.StatusesService.ReadModel.EventHandlers;
using System;
using WorkflowManager.Common.ApplicationInitializer;

namespace WorkflowManager.StatusesService.API
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
            services.AddRabbitMq();

            services.AddRabbitMq();
            services.AddEventStore(NEventStore.Logging.LogLevel.Info, "MsSqlDatabase");
            services.AddServiceSwaggerUI();
            services.AddReadModelStore<StatusesContext>("MsSqlDatabase");
            services.AddReadModelRepository<StatusModel, StatusReadModelRepository>();

            services.AddCommandHandler<CreateStatusCommand, CreateStatusCommandHandler>()
                    .AddCommandHandler<UpdateStatusCommand, UpdateStatusCommandHandler>()
                    .AddCommandHandler<RemoveStatusCommand, RemoveStatusCommandHandler>()

                    .AddEventHandler<StatusCreatedEvent, StatusCreatedEventHandler>()
                    .AddEventHandler<StatusNameUpdatedEvent, StatusNameUpdatedEventHandler>()
                    .AddEventHandler<StatusProcessIdUpdatedEvent, StatusProcessIdUpdatedEventHandler>()
                    .AddEventHandler<StatusRemovedEvent, StatusRemovedEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServiceConfiguration.InjectCommonMiddlewares(app, env);

            app.UseRabbitMq()
                .SubscribeCommand<CreateStatusCommand>()
                .SubscribeCommand<UpdateStatusCommand>()
                .SubscribeCommand<RemoveStatusCommand>()

                .SubscribeEvent<StatusCreatedEvent>()
                .SubscribeEvent<StatusNameUpdatedEvent>()
                .SubscribeEvent<StatusProcessIdUpdatedEvent>()
                .SubscribeEvent<StatusRemovedEvent>();
        }
    }
}
