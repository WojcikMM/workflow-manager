using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkflowManager.IdentityService.API.DTO;
using WorkflowManager.IdentityService.API.Services;

namespace WorkflowManager.IdentityService.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService) => _identityService = identityService;

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Check()
        {
            return Ok("User authorized");
        }


        [AllowAnonymous]
        [HttpPost("sign-up")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public IActionResult SignUp([FromBody] RegisterUserCommand command)
        {
            _identityService.RegisterUser(command.Name, command.Password);
            return NoContent();
        }


        [AllowAnonymous]
        [HttpPost("sign-in")]
        [ProducesResponseType(typeof(TokenDTO), (int)HttpStatusCode.OK)]
        public IActionResult SignIn([FromBody] SignInCommand command)
        {
            try
            {
                var token = _identityService.SignIn(command.Name, command.Password);
                return Ok(new TokenDTO(token));
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet("token-renew")]
        [ProducesResponseType(typeof(TokenDTO), (int)HttpStatusCode.OK)]
        public IActionResult RenewToken()
        {
            Guid userId = new Guid(HttpContext.User.Identity.Name);
            var token =  _identityService.RenewToken(userId);
            return Ok(new TokenDTO(token));            
        }


    }
}