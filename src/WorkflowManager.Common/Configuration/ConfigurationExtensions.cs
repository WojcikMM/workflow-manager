using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WorkflowManager.Common.Configuration
{
    public static class ConfigurationExtensions
    {

        public static T GetValue<T>(this IServiceCollection services, string ConfigPath)
        {
            return services.GetConfiguration().GetValue<T>(ConfigPath);
        }

        public static T GetOptions<T>(this IConfiguration configuration,
                                            string sectionName,
                                            bool failIfNotExists = false)
            where T : class, new()
        {
            T option = new T();
            IConfigurationSection section = configuration.GetSection(sectionName);
            if (!section.Exists() && failIfNotExists)
            {
                throw new System.Exception($"Cannot get configuration for: {sectionName}.");
            }
            section.Bind(option);
            return option;
        }

        public static T GetOptions<T>(this IServiceCollection services,
                                           string sectionName,
                                           bool failIfNotExists = false)
            where T : class, new()
        {
            return services.GetConfiguration().GetOptions<T>(sectionName, failIfNotExists);
        }

        public static string GetConnectionString(this IServiceCollection services, string connectionStringName)
        {
            return services.GetConfiguration().GetConnectionString(connectionStringName);
        }

        public static string GetIdentityUrl(this IServiceCollection services)
        {
            return services.GetValue<string>("IdentityUrl");
        }

        private static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IConfiguration>();
        }
    }
}
