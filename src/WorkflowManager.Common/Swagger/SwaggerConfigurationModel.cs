using System.Collections.Generic;

namespace WorkflowManager.Common.Swagger
{
    public class SwaggerConfigurationModel
    {
        public string IdentityServiceUrl { get; set; }

        public Dictionary<string, string> Scopes { get; set; }

    }
}
