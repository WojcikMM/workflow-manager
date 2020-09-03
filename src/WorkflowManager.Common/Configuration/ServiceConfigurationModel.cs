using System.Reflection;

namespace WorkflowManager.Common.ApplicationInitializer
{
    public class ServiceConfigurationModel
    {
        public string Name { get; set; }

        public string ServiceName =>
            string.IsNullOrWhiteSpace(Name) ? "Service" : $"{Name} Service";

        public string ServiceVersion =>
            $"v{Assembly.GetEntryAssembly().GetName().Version.Major}";

        public string ServiceNameWithVersion => $"{ServiceName} {ServiceVersion}";

        public AssemblyName ServiceAssemblyInformations => Assembly.GetEntryAssembly().GetName();

        public bool IsApi { get; set; } = true;

        public bool IsAzureServiceApp { get; set; }

    }
}
