using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WorkflowManager.Common.Controllers;
using WorkflowManager.IdentityService.API.Commands;

namespace WorkflowManager.IdentityService.API.Controllers
{
    public class RolesController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager) =>
            _roleManager = roleManager;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IdentityRole>), StatusCodes.Status200OK)]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Collection(roles);
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(IdentityRole), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRole([FromRoute]Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return Single(role);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleCommand command)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(command.RoleName));

            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync(command.RoleName);
                return Created($"api/roles/{role.Id}", null);
            }

            AddErrorsToModelState(result);

            return ValidationProblem(ModelState);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRole([FromRoute]Guid id, [FromBody]UpdateRoleCommand command)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                role.Name = command.RoleName;
                var result = await _roleManager.UpdateAsync(role);
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
        public async Task<IActionResult> RemoveRole([FromRoute]Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return NoContent();
                }

                AddErrorsToModelState(result);
                return ValidationProblem();
            }
            return NotFound();
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