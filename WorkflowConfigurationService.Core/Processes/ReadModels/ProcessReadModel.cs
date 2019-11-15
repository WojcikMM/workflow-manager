using System;
using CQRS.Template.ReadModel;

namespace WorkflowConfigurationService.Core.ReadModel.Models
{
    public class ProcessReadModel : IReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}
