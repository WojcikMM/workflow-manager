using WorkflowManager.Common.ApplicationInitializer;

namespace WorkflowManager.StatusesService.API
{
    public class Program
    {
        public static void Main(string[] args) =>
            ServiceConfiguration.Initialize<Startup>(args);
    }
}
