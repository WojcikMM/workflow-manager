using WorkflowManager.Common.ApplicationInitializer;

namespace WorkflowManager.NotificationsService.API
{
    public class Program
    {
        public static void Main(string[] args) =>
            ServiceConfiguration.Initialize<Startup>(args);
    }
}
