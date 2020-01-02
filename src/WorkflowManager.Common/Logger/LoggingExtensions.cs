using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowManager.Common.Configuration;

namespace WorkflowManager.Common.Logger
{
    public static class LoggingExtensions
    {

        public static IWebHostBuilder AddDotnetCoreLogs(this IWebHostBuilder webHostBuilder, string sectionName = "Logging")
        {
            webHostBuilder.ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection(sectionName));
                logging.AddConsole();
                logging.AddDebug();
                logging.AddEventSourceLogger(); 
            });
            return webHostBuilder;
        }

    }
}
