using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using WorkflowManager.Common.Configuration;
using WorkflowManager.Common.Swagger;

namespace WorkflowManager.Common.ApplicationInitializer
{
    public class ServiceConfiguration
    {
        public static void Initialize<TStartup>(string[] args)
            where TStartup : class
        {

            ILogger logger = LoggerFactory.Create(x =>
            {
                x.AddConsole();
                x.AddConfiguration();
            }).CreateLogger("Boot");

            ServiceConfigurationModel serviceInformations = GetServiceInformations();
            try
            {
                Host.CreateDefaultBuilder(args)
                  .ConfigureWebHostDefaults(webBuilder =>
                  {
                      var serviceInfo = GetServiceInformations(true);

                      webBuilder.UseStartup<TStartup>()
                      .ConfigureLogging(logConfig =>
                      {
                          if (serviceInfo.IsAzureServiceApp)
                          {
                              logConfig.AddAzureWebAppDiagnostics();
                          }
                      })
                      .CaptureStartupErrors(true)
                      .Configure((context, app) =>
                      {

                          var env = context.HostingEnvironment;
                          logger.LogInformation($"{serviceInformations.ServiceNameWithVersion} starting ...");

                          if (env.IsEnvironment("Docker") || env.IsEnvironment("Compose") || env.IsDevelopment())
                          {
                              app.UseDeveloperExceptionPage();
                              //  app.UseDatabaseErrorPage();
                          }
                          app.UseRouting();
                          app.UseAuthentication();
                          app.UseAuthorization();
                          app.UseServiceSwaggerUI();
                          app.RegisterCorsMiddleware();

                          app.UseEndpoints(endpoints =>
                          {
                              if (serviceInfo.IsApi)
                              {
                                  endpoints.MapControllers();
                              }
                              else
                              {
                                  endpoints.MapDefaultControllerRoute();
                              }
                          });


                      });
                  })
                  .Build()
                  .Run();
                logger.LogInformation($"{serviceInformations.ServiceNameWithVersion}) started successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{serviceInformations.ServiceNameWithVersion}) stopped by error.");
                throw;
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
    }
}
