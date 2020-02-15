using WorkflowManager.Common.ApplicationInitializer;

namespace IdentityServerAspNetIdentity
{
    public class Program
    {
        public static void Main(string[] args) =>
            ServiceConfiguration.Initialize<Startup>(args);
    }
}
