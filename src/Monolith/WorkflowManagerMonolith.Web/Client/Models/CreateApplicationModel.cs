using System;
using System.ComponentModel.DataAnnotations;

namespace WorkflowManagerMonolith.Web.Client.Models
{
    public class CreateApplicationModel
    {
        [Required(ErrorMessage = "This field is required.")]
        public string ApplicationNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public Guid InitialTransaction { get; set; }
    }
}
