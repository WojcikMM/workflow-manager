using System.ComponentModel.DataAnnotations;

namespace WorkflowManager.IdentityService.API.Commands
{
    public class UpdateRoleCommand
    {
        [Required]
        [MinLength(2)]
        public string RoleName { get; set; }
    }
}
