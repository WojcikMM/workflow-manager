using Microsoft.Extensions.Configuration;

namespace WorkflowManager.Common.Configuration
{
    public static class ConfigurationExtensions
    {
        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var option = new T();
            var section = configuration.GetSection(sectionName);
            if (!section.Exists())
            {
                throw new System.Exception($"Cannot get configuration for: {sectionName}.");
            }
            section.Bind(option);
            return option;
        }
    }
}
