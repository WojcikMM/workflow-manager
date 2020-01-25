using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.Common.Swagger;
using WorkflowManager.CQRS.Domain.EventHandlers;
using WorkflowManager.OperationsStorage.Api.Services;
using WorkflowManager.OperationsStorage.API.Handlers;

namespace WorkflowManager.OperationsStorage.Api
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
            services.AddTransient<IOperationsStorage, OperationStorage>();
            services.AddTransient<IOperationPublisher, OperationPublisher>();
            services.AddSingleton<IDistributedCache, Microsoft.Extensions.Caching.Distributed.MemoryDistributedCache>();
            services.AddControllers();
            services.AddRabbitMq();
            services.AddServiceSwaggerUI();

            services.AddTransient(typeof(IEventHandler<>), typeof(GenericEventHandler<>));

           // services.AddEventHandler
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseRabbitMq().SubscribeAllMessages();
            app.UseServiceSwaggerUI();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
