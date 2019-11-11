using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkflowConfigurationService.Infrastructure.Commands.Application;

namespace WorkflowConfigurationService.API.Controllers
{
    public class ApplicationsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationCommand createApplicationCommand)
        {
            return Ok();
        }
    }
}