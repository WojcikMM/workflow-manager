using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.ApplicationInitializer;
using WorkflowManager.Common.Authentication;
using WorkflowManager.Common.Configuration;
using WorkflowManager.Common.MassTransit;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.Common.Messages.Events.Statuses;
using WorkflowManager.Common.Swagger;
using WorkflowManager.OperationsStorage.Api.Services;
using WorkflowManager.OperationsStorage.API.Handlers;

namespace WorkflowManager.OperationsStorage.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ServiceConfiguration.InjectCommonServices(services);
            services.AddCorsAbility();
            services.AddServiceSwaggerUI();
            services.AddClientAuthentication();

            services.AddMasstransitWithReflection(GetConsumerTypesToRegister());

            services.AddTransient<IOperationsStorage, OperationStorage>();
            services.AddTransient<IOperationPublisher, OperationPublisher>();
            services.AddSingleton<IDistributedCache, MemoryDistributedCache>();
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

                {typeof(ProcessCreatedEvent), typeof(ProcessesEventHandler) },
                {typeof(ProcessNameUpdatedEvent), typeof(ProcessesEventHandler) },

                {typeof(StatusCreatedEvent), typeof(StatusesEventHandler) },
                {typeof(StatusNameUpdatedEvent), typeof(StatusesEventHandler) },
                {typeof(StatusProcessIdUpdatedEvent), typeof(StatusesEventHandler) }
            };
        }
    }
}
