using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowManager.Common.MsSQL
{
    public class MsSqlConfigurationModel
    {
        public string DataSource { get; set; }
        public string DatabaseName { get; set; }
        public string UserId { get; set; }
        public string UserPassword { get; set; }
        public int? ConnectTimeout { get; set; }
        public Microsoft.Data.SqlClient.ApplicationIntent? ApplicationIntent { get; set; }
        public bool? MultiSubnetFailover { get; set; }
    }
}
