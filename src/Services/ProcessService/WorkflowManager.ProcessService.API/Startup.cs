using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkflowManager.Common.CQRSHandlers;
using WorkflowManager.Common.EventStore;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.Common.Messages.Events.Processes.Complete;
using WorkflowManager.Common.Messages.Events.Processes.Rejected;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.Common.ReadModelStore;
using WorkflowManager.Common.Swagger;
using WorkflowManager.ProcessService.ReadModel;
using WorkflowManager.ProcessService.ReadModel.ReadDatabase;
using WorkflowManager.ProductService.Core.CommandHandlers;
using WorkflowManager.ProductService.Core.EventHandlers;

namespace WorkflowManager.ProductService.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRabbitMq();
            services.AddEventStore(NEventStore.Logging.LogLevel.Info, "MsSqlDatabase");
            services.AddServiceSwaggerUI();
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseRabbitMq()
                .SubscribeCommand<CreateProcessCommand, ProcessCreateRejectedEvent>()
                .SubscribeCommand<UpdateProcessCommand, ProcessUpdateRejectedEvent>()
                .SubscribeCommand<RemoveProcessCommand, ProcessRemoveRejectedEvent>()

                .SubscribeEvent<ProcessCreatedEvent, ProcessCreateRejectedEvent, ProcessCreateCompleteEvent>()
                .SubscribeEvent<ProcessNameUpdatedEvent, ProcessUpdateRejectedEvent, ProcessUpdateCompleteEvent>()
                .SubscribeEvent<ProcessRemovedEvent, ProcessRemoveRejectedEvent, ProcessRemoveCompleteEvent>();

            app.UseAuthorization();
            app.UseServiceSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
