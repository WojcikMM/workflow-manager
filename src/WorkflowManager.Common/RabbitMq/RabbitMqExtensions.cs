using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using RawRabbit.vNext.Disposable;
using System;
using System.Collections.Generic;
using WorkflowManager.Common.Configuration;

namespace WorkflowManager.Common.RabbitMq
{
    public static class RabbitMqExtensions
    {
        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app) =>
            new BusSubscriber(app);

        public static void AddRabbitMq(this IServiceCollection services, string sectionName = "RabbitMq")
        {
            var options = services.GetOptions<RabbitMqConfigurationModel>(sectionName);

            RawRabbitConfiguration config = new RawRabbitConfiguration()
            {
                AutoCloseConnection = options.AutoCloseConnection,
                Username = options.Username,
                Password = options.Password,
                Port = options.Port,
                VirtualHost = options.VirtualHost,
                Hostnames = new List<string>() { options.Hostname },
                PublishConfirmTimeout = TimeSpan.FromSeconds(options.PublishConfirmTimeout),
                RequestTimeout = TimeSpan.FromSeconds(options.RequestTimeout)
            };

            for (int retryNumber = 1; retryNumber <= options.RetryConnectCount; retryNumber++)
            {
                try
                {
                    IBusClient busClient = BusClientFactory.CreateDefault(config);
                    services.AddSingleton<IBusClient>(busClient);
                    break;
                }
                catch(Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex, $"Cannot connect to rabbitmq, {config.Hostnames.Join(",")} | {config.Username}:{config.Password}");


                    if (retryNumber == options.RetryConnectCount)
                    {
                        throw;
                    }
                    System.Threading.Thread.Sleep(options.RetryConnectInterval);
                }
            }
            

            services.AddTransient<IBusPublisher, BusPublisher>();
        }
    }
}
