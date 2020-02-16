using WorkflowManager.Common.ApplicationInitializer;

namespace WorkflowManager.OperationsStorage.Api
{
    public class Program
    {
        public static void Main(string[] args) =>
             ServiceConfiguration.Initialize<Startup>(args);
    }
}
