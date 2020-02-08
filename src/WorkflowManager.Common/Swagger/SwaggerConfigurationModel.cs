using System.Collections.Generic;

namespace WorkflowManager.Common.Swagger
{
    public class SwaggerConfigurationModel
    {
        public string ServiceName { get; set; }
        public string Version { get; set; }

        public string IdentityServiceUrl { get; set; }

        public Dictionary<string, string> Scopes { get; set; }

    }
}
