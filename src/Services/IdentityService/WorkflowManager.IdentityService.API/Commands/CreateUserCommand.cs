using System.ComponentModel.DataAnnotations;

namespace WorkflowManager.IdentityService.API.Commands
{
    public class CreateUserCommand
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(2)]
        public string UserName { get; set; }

        [MinLength(9)]
        [MaxLength(15)]
        [RegularExpression(@"^[\d-]+$", ErrorMessage = "Type only numbers or numbers with dashes.")]
        public string PhoneNumber { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        [RegularExpression(@"^\S+$", ErrorMessage = "Password should not contains whitespaces.")]
        public string InitialPassword { get; set; } = "Initial1!";
    }
}