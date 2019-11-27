using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowManager.Common.CQRSHandlers;
using WorkflowManager.Common.EventStore;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.Common.ReadModelStore;
using WorkflowManager.Common.Swagger;
using WorkflowManager.ProcessService.API.Middlewares;
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
            services.AddEventStore();
            services.AddServiceSwaggerUI();
            services.AddReadModelStore<ProcessesContext>();
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();
            app.UseRabbitMq()
                .SubscribeCommand<CreateProcessCommand>()
                .SubscribeCommand<UpdateProcessCommand>()
                .SubscribeCommand<RemoveProcessCommand>()

                .SubscribeEvent<ProcessCreatedEvent>()
                .SubscribeEvent<ProcessNameUpdatedEvent>()
                .SubscribeEvent<ProcessRemovedEvent>();

            app.UseAuthorization();
            app.UseServiceSwaggerUI();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
