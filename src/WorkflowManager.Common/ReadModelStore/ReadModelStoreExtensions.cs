using WorkflowManager.CQRS.ReadModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.Configuration;

namespace WorkflowManager.Common.ReadModelStore
{
    public static class ReadModelStoreExtensions
    {
        public static void AddReadModelStore<TContext>(this IServiceCollection services, string connectionStringName = "ReadDatabase") where TContext : DbContext
        {
            var connectionString = services.GetConnectionString(connectionStringName);

            services.AddDbContext<TContext>(options => options.UseSqlServer(connectionString),
                optionsLifetime: ServiceLifetime.Transient, contextLifetime: ServiceLifetime.Transient);
        }

        public static void AddReadModelRepository<TReadModel, TReadModelRepository>(this IServiceCollection services)
            where TReadModel : IReadModel, new() where TReadModelRepository : IReadModelRepository<TReadModel>
        {
            services.AddScoped(typeof(IReadModelRepository<TReadModel>), typeof(TReadModelRepository));
        }
    }
}
