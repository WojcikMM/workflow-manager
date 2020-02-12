using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowManager.Common.Authentication
{
    public class AuthenticationConfigurationModel
    {
        public string Audience { get; internal set; }
        public string Authority { get; internal set; }
        public string MetadataAddress { get; internal set; }
    }
}
