using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowManager.APIGateway
{
    public class GatewayServicesConfigurationModel
    {
        public string ProcessServiceUrl { get; set; }
        public string StatusServiceUrl { get; set; }
        public string SignalRServiceUrl { get; set; }
        public string OperationsServiceUrl { get; set; }
    }
}
