using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WorkflowManager.ProcessesService.API
{
    public interface IStartup
    {
        IConfiguration Configuration { get; }

        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
        void ConfigureServices(IServiceCollection services);
    }
}