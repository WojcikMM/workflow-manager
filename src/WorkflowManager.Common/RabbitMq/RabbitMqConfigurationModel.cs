using System;
using System.Collections.Generic;

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
        public double PublishConfirmTimeout { get; set; } = 500;
    }
}
