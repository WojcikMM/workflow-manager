using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkflowManagerMonolith.Core.Repositories;
using WorkflowManagerMonolith.Application.EntityFramework.Repositories;
using WorkflowManagerMonolith.Infrastructure.Services;
using WorkflowManagerMonolith.Application.Applications;
using WorkflowManagerMonolith.Application.Transactions;
using WorkflowManagerMonolith.Infrastructure.EntityFramework;

namespace WorkflowManagerMonolith.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<WorkflowManagerDbContext>(options =>
                    options.UseInMemoryDatabase("Database"));

            services.AddEntityFrameworkInMemoryDatabase();

           services.AddSwaggerGen();

            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();

            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<ITransactionService, TrasactionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
           {
               cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkflowManager.Monolith");
               cfg.RoutePrefix = ""; // TODO: Use "swagger" when go to prod
           });

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}