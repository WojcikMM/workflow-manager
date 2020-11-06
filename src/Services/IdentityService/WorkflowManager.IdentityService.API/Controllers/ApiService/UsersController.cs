using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkflowManager.Common.Controllers;
using WorkflowManager.IdentityService.API.Commands;

namespace WorkflowManager.IdentityService.API.Controllers.ApiService
{
    public class UsersController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager) =>
            _userManager = userManager;

        [HttpGet]
        [ProducesResponseType(typeof(IdentityUser), StatusCodes.Status200OK)]
        public IActionResult GetUsers() =>
            Collection(_userManager.Users.ToList());

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IdentityUser), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserAsync([FromRoute]Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            return Single(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var user = new IdentityUser()
            {
                Email = command.Email,
                UserName = command.UserName,
                PhoneNumber = command.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, command.InitialPassword);
            if (!result.Succeeded)
            {
                AddErrorsToModelState(result);
                return ValidationProblem();
            }

            return Created($"api/users/{user.Id}", null);

        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser([FromRoute]Guid id, [FromBody]UpdateUserCommand command)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                if (!string.IsNullOrWhiteSpace(command.Email))
                {
                    user.Email = command.Email;
                }

                if (!string.IsNullOrWhiteSpace(command.PhoneNumber))
                {
                    user.PhoneNumber = command.PhoneNumber;
                }

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return NoContent();
                }

                AddErrorsToModelState(result);
                return ValidationProblem();
            }
            return NotFound();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveUser([FromRoute]Guid id)
        {
            var role = await _userManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                var result = await _userManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return NoContent();
                }

                AddErrorsToModelState(result);
                return ValidationProblem();
            }
            return NotFound();
        }

        [HttpGet("{id}/roles")]
        [ProducesResponseType(typeof(IEnumerable<string>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserRoles([FromRoute]Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null)
            {
                return NotFound();
            }
            var roles = await _userManager.GetRolesAsync(user);
            
            return Ok(roles);
        }

        [HttpPatch("{id}/roles/add")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRolesToUser([FromRoute]Guid id, [FromBody]AppendUserRoleCommand roleDto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null)
            {
                return NotFound();
            }

            var result = await _userManager.AddToRoleAsync(user, roleDto.RoleName);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return ValidationProblem();
        }

        [HttpPatch("{id}/roles/remove")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveRolesToUser([FromRoute]Guid id, [FromBody]RemoveUserRoleCommand roleDto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleDto.RoleName);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return ValidationProblem();
        }

        private void AddErrorsToModelState(IdentityResult result)
        {
            if (result.Succeeded)
            {
                throw new Exception("Invalid flow. Result successed");
            }
            result.Errors.ToList().ForEach(e =>
            {
                ModelState.AddModelError(e.Code, e.Description);
            });
        }

    }
}