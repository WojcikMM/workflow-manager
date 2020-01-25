using System;

namespace WorkflowManager.Gateway.API.DTOs
{
    public class OperationDTO
    {
        public Guid AggregateId { get; set; }
        public string Status { get; set; }
    }
}
