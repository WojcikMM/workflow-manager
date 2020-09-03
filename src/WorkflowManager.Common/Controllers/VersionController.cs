using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WorkflowManager.Common.ApplicationInitializer;

using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace WorkflowManager.Common.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class VersionController : ControllerBase
    {
        private string ServiceName { get; set; }
        private string ServiceVersion { get; set; }
        public VersionController(IOptions<ServiceConfigurationModel> options)
        {
            ServiceName = options.Value.ServiceName;
            ServiceVersion = options.Value.ServiceVersion;
        }


        [HttpGet("")]
        [ProducesResponseType(typeof(VersionResultModel), (int)HttpStatusCode.OK)]
        public IActionResult GetVersion()
        {
            return Ok(new VersionResultModel(ServiceName, ServiceVersion));
        }

    }

    public class VersionResultModel
    {
        public VersionResultModel(string ServiceName, string Version)
        {
            this.ServiceName = ServiceName;
            this.Version = Version;
        }

        public string ServiceName { get; set; }
        public string Version { get; set; }
    }
}