using System.ComponentModel.DataAnnotations;

namespace WorkflowManagerMonolith.Web.Client.Models
{
    public class SearchModel
    {
        [RegularExpression(@"\w{8}(\-\w{4}){4}\w{8}", ErrorMessage = "Wrong application id.")]
        public string ApplicationId { get; set; }

        [RegularExpression(@"\w{8}(\-\w{4}){4}\w{8}", ErrorMessage = "Please select correct status")]
        public string StatusId { get; set; }
    }
}
