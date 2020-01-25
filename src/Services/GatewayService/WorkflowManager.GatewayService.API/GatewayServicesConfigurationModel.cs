namespace WorkflowManager.Gateway.API
{
    public class GatewayServicesConfigurationModel
    {
        public string ProcessServiceUrl { get; set; }
        public string StatusServiceUrl { get; set; }
        public string SignalRServiceUrl { get; set; }
        public string OperationsServiceUrl { get; set; }
    }
}
