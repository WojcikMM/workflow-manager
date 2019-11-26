using CQRS.Template.ReadModel;
using System;

namespace WorkflowManager.ProcessService.ReadModel.ReadDatabase
{
    public class ProcessModel : IReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; }
    }
}
