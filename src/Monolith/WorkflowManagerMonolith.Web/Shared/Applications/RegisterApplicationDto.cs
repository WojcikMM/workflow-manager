using System;
using System.ComponentModel.DataAnnotations;

namespace WorkflowManagerMonolith.Web.Shared.Applications
{
    public class RegisterApplicationDto
    {
        [Required(ErrorMessage = "This field is required.")]
        public string ApplicationNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public Guid? InitialTransactionId { get; set; }
    }
}
