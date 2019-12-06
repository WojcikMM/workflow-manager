using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using RawRabbit.vNext.Disposable;
using System;

namespace WorkflowManager.Common.RabbitMq
{
    public static class RabbitMqExtensions
    {
        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app) =>
            new BusSubscriber(app);

        public static void AddRabbitMq(this IServiceCollection services)
        {
            RawRabbitConfiguration config = new RawRabbitConfiguration()
            {
                AutoCloseConnection = false,
                Username = "guest",
                Password = "guest",
                Port = 5372,
                VirtualHost = "/",
                Hostnames = { "rabbitmq" },
                PublishConfirmTimeout = TimeSpan.FromMilliseconds(500)
            };

            IBusClient busClient = BusClientFactory.CreateDefault(config);
            services.AddSingleton<IBusClient>(busClient);

            services.AddTransient<IBusPublisher, BusPublisher>();
        }
    }
}
