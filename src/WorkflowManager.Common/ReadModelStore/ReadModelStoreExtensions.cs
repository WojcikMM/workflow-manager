using WorkflowManager.CQRS.ReadModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WorkflowManager.Common.ReadModelStore
{
    public static class ReadModelStoreExtensions
    {
        private static readonly string _readDbConnectionStringName = "ServiceReadDatabase";
        public static void AddReadModelStore<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            string connectionString;
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                IConfiguration configuration = serviceProvider.GetService<IConfiguration>();
                connectionString = configuration.GetConnectionString(_readDbConnectionStringName);
            }
            if (connectionString is null)
            {
                throw new ApplicationException("Cannot find read model connectionString value.");
            }

            services.AddDbContext<TContext>(options => options.UseSqlServer(connectionString),
                optionsLifetime: ServiceLifetime.Transient, contextLifetime: ServiceLifetime.Transient);
        }

        public static void AddReadModelRepository<TReadModel, TReadModelRepository>(this IServiceCollection services)
            where TReadModel : IReadModel, new() where TReadModelRepository : IReadModelRepository<TReadModel>
        {
            services.AddTransient(typeof(IReadModelRepository<TReadModel>), typeof(TReadModelRepository));
        }
    }
}
