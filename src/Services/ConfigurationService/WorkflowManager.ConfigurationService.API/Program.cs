using WorkflowManager.Common.ApplicationInitializer;

namespace WorkflowManager.ConfigurationService.API
{
    public class Program
    {
        public static void Main(string[] args) =>
           ServiceConfiguration.Initialize<Startup>(args);
    }
}
