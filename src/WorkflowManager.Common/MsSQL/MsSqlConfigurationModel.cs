using Microsoft.Data.SqlClient;

namespace WorkflowManager.Common.MsSQL
{
    public class MsSqlConfigurationModel
    {
        public string DataSource
        {
            get
            {
                return this.Port.HasValue ? $"{this.Server},{this.Port}" : this.Server;
            }
        }
        public string Server { get; set; } = "localhost";
        public int? Port { get; set; } = 1453;
        public string DatabaseName { get; set; } = "db";
        public string UserId { get; set; } = "sa";
        public string UserPassword { get; set; } = "sa";
        public int ConnectTimeout { get; set; } = 30;
        public ApplicationIntent ApplicationIntent { get; set; } = ApplicationIntent.ReadWrite;
        public bool MultiSubnetFailover { get; set; } = false;
    }
}
