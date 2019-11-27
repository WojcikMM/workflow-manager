using Microsoft.Extensions.Configuration;

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
    }
}
