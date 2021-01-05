using System;

namespace WorkflowManagerMonolith.Application.Statuses.DTOs
{
    public class CreateStatusCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
