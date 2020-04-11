using System.ComponentModel.DataAnnotations;

namespace WorkflowManager.IdentityService.API.Commands
{
    public class RemoveUserRoleCommand
    {
        [Required]
        [MinLength(2)]
        public string RoleName { get; set; }
    }
}
