using System;
using NLog.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using WorkflowManager.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using WorkflowManager.Common.Swagger;
using NLog.Common;
using System.IO;

namespace WorkflowManager.Common.ApplicationInitializer
{
    public class ServiceConfiguration
    {
        public static void Initialize<TStartup>(string[] args)
            where TStartup : class
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            InternalLogger.LogFile =
               Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "nlog-internals.txt");


            ServiceConfigurationModel serviceInformations = GetServiceInformations();
            try
            {
                logger.Info($"{serviceInformations.ServiceNameWithVersion}) started successfully");

                Host.CreateDefaultBuilder(args)
                  .ConfigureWebHostDefaults(webBuilder =>
                  {
                      webBuilder.UseStartup<TStartup>()
                      .ConfigureLogging((context, logging) =>
                      {
                          logging.ClearProviders();
                          logging.SetMinimumLevel(LogLevel.Trace);
                      })
                      .UseNLog();
                  }).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{serviceInformations.ServiceNameWithVersion}) stopped by error.");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }


        /// <summary>
        /// Gets data from "Service" section and Service version form service assembly
        /// </summary>
        /// <returns></returns>
        public static ServiceConfigurationModel GetServiceInformations(bool requireServiceName = true)
        {

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false)
               .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
               .Build();

            var serviceConfiguration =
                config.GetOptions<ServiceConfigurationModel>("Service", requireServiceName);

            if (requireServiceName && string.IsNullOrWhiteSpace(serviceConfiguration.Name))
            {
                throw new ArgumentNullException(nameof(serviceConfiguration.Name),
                    "Missing service name in \"Serviec\" config section.");
            }

            return serviceConfiguration;
        }


        public static void InjectCommonMiddlewares(IApplicationBuilder app,
                                                   IWebHostEnvironment env,
                                                   bool isApi = true)
        {
            if (env.IsEnvironment("Docker") || env.IsEnvironment("Compose") || env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseServiceSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                if (isApi)
                {
                    endpoints.MapControllers();
                }
                else
                {
                    endpoints.MapDefaultControllerRoute();
                }
            });

        }
    }
}
