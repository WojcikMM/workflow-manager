using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowManager.Common.CQRSHandlers;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.Common.Messages.Events.Saga;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.NotificationsService.API.CommandHandlers;
using WorkflowManager.NotificationsService.API.HubConfig;

namespace WorkflowManager.NotificationsService.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
            services.AddSignalRCore();
            services.AddControllers();
            services.AddRabbitMq();
            services.AddEventHandler<BaseCompleteEvent, CompleteEventsHandler>()
                    .AddEventHandler<BaseRejectedEvent, CompleteEventsHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseRabbitMq()
                .SubscribeEvent<ProcessCreatedEvent>()
                .SubscribeEvent<ProcessNameUpdatedEvent>()
                .SubscribeEvent<ProcessRemovedEvent>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<EventHub>("/events");
            });

        }
    }
}