using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WorkflowConfigurationService.API.Controllers
{
    public class ApplicationsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateApplication([FromBody] dynamic createApplicationCommand)
        {
            return Ok();
        }
    }
}