using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.Authentication;
using WorkflowManager.Common.EventStore;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.Common.ReadModelStore;
using WorkflowManager.Common.Swagger;
using WorkflowManager.ProcessesService.ReadModel;
using WorkflowManager.ProcessesService.ReadModel.ReadDatabase;
using WorkflowManager.Common.ApplicationInitializer;
using WorkflowManager.Common.Cors;
using WorkflowManager.Common.Messages.Commands.Processes;
using System.Threading.Tasks;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.Common.MassTransit;
using MassTransit;

namespace WorkflowManager.ProcessesService.API
{

    public class SampleConsumer : IConsumer<CreateProcessCommand>
    {
        private IReadModelRepository<ProcessModel> _logger;

        public SampleConsumer(IReadModelRepository<ProcessModel> logger)
        {
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<CreateProcessCommand> context)
        {
            var xxx = await _logger.GetAllAsync();
        }
    }



    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCorsAbility();
            // services.AddRabbitMq();
            services.AddEventStore(NEventStore.Logging.LogLevel.Info, "MsSqlDatabase");
            services.AddServiceSwaggerUI();
            services.AddClientAuthentication();

            services.AddReadModelStore<ProcessesContext>("MsSqlDatabase");
            services.AddReadModelRepository<ProcessModel, ProcessReadModelRepository>();


            // AZURE SERVICE BUS TEST

            services.AddMasstransit();

            services.AddTransient<IBusPublisher>(x => new BusPublisherDummy());

            //services.AddCommandHandler<CreateProcessCommand, CreateProcessCommandHandler>()
            //        .AddCommandHandler<UpdateProcessCommand, UpdateProcessCommandHandler>()
            //        .AddCommandHandler<RemoveProcessCommand, RemoveProcessCommandHandler>()

            //        .AddEventHandler<ProcessCreatedEvent, ProcessCreatedEventHandler>()
            //        .AddEventHandler<ProcessNameUpdatedEvent, ProcessNameUpdatedEventHandler>()
            //        .AddEventHandler<ProcessRemovedEvent, ProcessRemovedEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServiceConfiguration.InjectCommonMiddlewares(app, env);
            //  app.RegisterCorsMiddleware();
            //app.UseRabbitMq()
            //    .SubscribeCommand<CreateProcessCommand, ProcessCreateRejectedEvent>()
            //    .SubscribeCommand<UpdateProcessCommand, ProcessUpdateRejectedEvent>()
            //    .SubscribeCommand<RemoveProcessCommand, ProcessRemoveRejectedEvent>()

            //    .SubscribeEvent<ProcessCreatedEvent, ProcessCreateRejectedEvent, ProcessCreateCompleteEvent>()
            //    .SubscribeEvent<ProcessNameUpdatedEvent, ProcessUpdateRejectedEvent, ProcessUpdateCompleteEvent>()
            //    .SubscribeEvent<ProcessRemovedEvent, ProcessRemoveRejectedEvent, ProcessRemoveCompleteEvent>();
        }
    }
}
