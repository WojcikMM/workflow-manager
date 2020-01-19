namespace WorkflowManager.Common.RabbitMq
{
    public class RabbitMqConfigurationModel
    {
        public bool AutoCloseConnection { get; set; } = true;
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public int Port { get; set; } = 5672;
        public string VirtualHost { get; set; } = "/";
        public string Hostname { get; set; } =  "localhost";
        public int PublishConfirmTimeout { get; set; } = 10;
        public int RequestTimeout { get; set; } = 360;
        /// <summary>
        /// Ammount of try estabilish connection to Rabbit Server
        /// </summary>
        public int RetryConnectCount { get; set; } = 3;
        /// <summary>
        /// Connection interval in miliseconds
        /// </summary>
        public int RetryConnectInterval { get; set; } = 60000;

    }
}
