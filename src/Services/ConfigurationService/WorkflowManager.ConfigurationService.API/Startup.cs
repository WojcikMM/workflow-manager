using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.ApplicationInitializer;
using WorkflowManager.Common.Authentication;
using WorkflowManager.Common.Configuration;
using WorkflowManager.Common.EventStore;
using WorkflowManager.Common.MassTransit;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.Common.Messages.Events.Statuses;
using WorkflowManager.Common.ReadModelStore;
using WorkflowManager.Common.Swagger;
using WorkflowManager.ConfigurationService.Core.CommandHandlers;
using WorkflowManager.ConfigurationService.Core.EventHandlers.Processes;
using WorkflowManager.ConfigurationService.ReadModel.EventHandlers.Statuses;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;
using WorkflowManager.ConfigurationService.ReadModel.Repositories;
using AutoMapper;

namespace WorkflowManager.ConfigurationService.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ServiceConfiguration.InjectCommonServices(services);
            services.AddCorsAbility();
            services.AddEventStore(NEventStore.Logging.LogLevel.Info, "MsSqlDatabase");
            services.AddServiceSwaggerUI();
            services.AddClientAuthentication();

            services.AddReadModelStore<ConfigurationDbContext>("MsSqlDatabase");
            services.AddReadModelRepository<ProcessModel, ProcessReadModelRepository>();
            services.AddReadModelRepository<StatusModel, StatusReadModelRepository>();

            services.AddAutoMapper(typeof(Startup));


            services.AddMasstransitWithReflection(GetConsumerTypesToRegister());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServiceConfiguration.InjectCommonMiddlewares(app, env);
        }

        private IDictionary<Type, Type> GetConsumerTypesToRegister()
        {
            return new Dictionary<Type, Type>()
            {
                {typeof(CreateProcessCommand), typeof(CreateProcessCommandHandler) },
                {typeof(UpdateProcessCommand), typeof(UpdateProcessCommandHandler) },

                {typeof(CreateStatusCommand), typeof(CreateStatusCommandHandler) },
                {typeof(UpdateStatusCommand), typeof(UpdateStatusCommandHandler) },

                {typeof(ProcessCreatedEvent), typeof(ProcessCreatedEventHandler) },
                {typeof(ProcessNameUpdatedEvent), typeof(ProcessNameUpdatedEventHandler) },

                {typeof(StatusCreatedEvent), typeof(StatusCreatedEventHandler) },
                {typeof(StatusNameUpdatedEvent), typeof(StatusNameUpdatedEventHandler) },
                {typeof(StatusProcessIdUpdatedEvent), typeof(StatusProcessIdUpdatedEventHandler) }

            };
        }
    }
}
