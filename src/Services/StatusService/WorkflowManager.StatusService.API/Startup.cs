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
using WorkflowManager.StatusService.ReadModel;
using WorkflowManager.StatusService.Core.CommandHandlers;
using WorkflowManager.StatusService.ReadModel.ReadDatabase;
using WorkflowManager.StatusService.ReadModel.EventHandlers;
using System;

namespace WorkflowManager.StatusService.API
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
            Console.WriteLine(env.EnvironmentName);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseRabbitMq()
                .SubscribeCommand<CreateStatusCommand>()
                .SubscribeCommand<UpdateStatusCommand>()
                .SubscribeCommand<RemoveStatusCommand>()

                .SubscribeEvent<StatusCreatedEvent>()
                .SubscribeEvent<StatusNameUpdatedEvent>()
                .SubscribeEvent<StatusProcessIdUpdatedEvent>()
                .SubscribeEvent<StatusRemovedEvent>();

            app.UseAuthorization();
            app.UseServiceSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
