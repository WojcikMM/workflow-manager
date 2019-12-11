using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.Configuration;

namespace WorkflowManager.Common.MsSQL
{
    internal class MsSqlExtensions
    {
        internal static string GetConnectionString(IServiceCollection services, string sectionName)
        {
            var options = services.GetOptions<MsSqlConfigurationModel>(sectionName);
            var connecionBuilder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder()
            {
                DataSource = options.DataSource,
                InitialCatalog = options.DatabaseName,
                UserID = options.UserId,
                Password = options.UserPassword,
                ConnectTimeout = options.ConnectTimeout,
                ApplicationIntent = options.ApplicationIntent,
                MultiSubnetFailover = options.MultiSubnetFailover
            };

            return connecionBuilder.ConnectionString;
        }
    }
}
