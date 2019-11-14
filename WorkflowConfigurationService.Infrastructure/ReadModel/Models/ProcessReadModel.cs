using System;
using CQRS.Template.ReadModel;

namespace WorkflowConfigurationService.Infrastructure.ReadModel.Models
{
    public class ProcessReadModel : IReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
