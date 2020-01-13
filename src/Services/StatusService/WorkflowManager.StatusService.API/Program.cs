using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using NLog.Web;
using Microsoft.Extensions.Logging;

namespace WorkflowManager.StatusService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Info("Process Service started successfully");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Status service stopped by error.");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>()
                     .ConfigureLogging((context, logging) =>
                     {
                         logging.ClearProviders();
                         logging.SetMinimumLevel(LogLevel.Trace);
                     })
                     .UseNLog();
                 });
    }
}
