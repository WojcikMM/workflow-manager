using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WorkflowManager.Common.Configuration
{
    public static class ConfigurationExtensions
    {
        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            T option = new T();
            IConfigurationSection section = configuration.GetSection(sectionName);
            if (!section.Exists())
            {
                throw new System.Exception($"Cannot get configuration for: {sectionName}.");
            }
            section.Bind(option);
            return option;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : class, new()
        {
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                IConfiguration configuration = serviceProvider.GetService<IConfiguration>();
                return configuration.GetOptions<T>(sectionName);
            }
        }
    }
}
