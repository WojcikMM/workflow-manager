using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.ApplicationInitializer;
using WorkflowManager.Common.EventStore;
using WorkflowManager.Common.ReadModelStore;
using WorkflowManager.Common.Swagger;
using WorkflowManager.StatusesService.ReadModel;
using WorkflowManager.StatusesService.ReadModel.ReadDatabase;

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
            services.AddEventStore(NEventStore.Logging.LogLevel.Info, "MsSqlDatabase");
            services.AddServiceSwaggerUI();
            services.AddReadModelStore<StatusesContext>("MsSqlDatabase");
            services.AddReadModelRepository<StatusModel, StatusReadModelRepository>();

            //services.AddCommandHandler<CreateStatusCommand, CreateStatusCommandHandler>()
            //        .AddCommandHandler<UpdateStatusCommand, UpdateStatusCommandHandler>()
            //        .AddCommandHandler<RemoveStatusCommand, RemoveStatusCommandHandler>()

            //        .AddEventHandler<StatusCreatedEvent, StatusCreatedEventHandler>()
            //        .AddEventHandler<StatusNameUpdatedEvent, StatusNameUpdatedEventHandler>()
            //        .AddEventHandler<StatusProcessIdUpdatedEvent, StatusProcessIdUpdatedEventHandler>()
            //        .AddEventHandler<StatusRemovedEvent, StatusRemovedEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    }
}
