using System.ComponentModel.DataAnnotations;

namespace WorkflowManager.IdentityService.API.Commands
{
    public class UpdateUserCommand
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(9)]
        [MaxLength(15)]
        [RegularExpression(@"^[\d-]+$", ErrorMessage = "Type only numbers or numbers with dashes.")]
        public string PhoneNumber { get; set; }
    }
}