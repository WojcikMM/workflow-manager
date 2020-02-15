using System.ComponentModel.DataAnnotations;

namespace WorkflowManager.IdentityService.API.Quickstart.Account
{
    public class RegistationViewModel
    {
        [Key]
        public int Key { get; set; }

        [Required]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(4)]
        public string Password { get; set; }

    }
}
