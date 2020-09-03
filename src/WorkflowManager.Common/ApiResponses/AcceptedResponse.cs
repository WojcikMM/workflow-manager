using System;

namespace WorkflowManager.Common.ApiResponses
{
    public class AcceptedResponse
    {
        public Guid AggregateId { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
