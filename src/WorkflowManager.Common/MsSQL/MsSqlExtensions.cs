using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Common.Configuration;

namespace WorkflowManager.Common.MsSQL
{
    internal class MsSqlExtensions
    {
        internal static string GetConnectionString(IServiceCollection services, string sectionName)
        {
            var options = services.GetOptions<MsSqlConfigurationModel>(sectionName);
            var connecionBuilder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder();

            connecionBuilder.DataSource = string.IsNullOrWhiteSpace(options.DataSource) ? "no_data_source" : options.DataSource;
            connecionBuilder.InitialCatalog = string.IsNullOrEmpty(options.DatabaseName) ? "no_database" :
                options.DatabaseName;
            connecionBuilder.UserID = string.IsNullOrWhiteSpace(options.UserId) ? "sa--" : options.UserId;
            connecionBuilder.Password = string.IsNullOrWhiteSpace(options.UserPassword) ? "no_password" : options.UserPassword;
            connecionBuilder.ConnectTimeout = options.ConnectTimeout ?? 30;
            connecionBuilder.ApplicationIntent = options.ApplicationIntent ?? Microsoft.Data.SqlClient.ApplicationIntent.ReadOnly;
            connecionBuilder.MultiSubnetFailover = options.MultiSubnetFailover ?? false;



            return connecionBuilder.ConnectionString;
        }
    }
}
