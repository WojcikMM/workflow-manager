using System;

namespace WorkflowManager.Common.ApiResponses
{
    public class CorrelationIdResponseDTO
    {
        public CorrelationIdResponseDTO()
        {
            CorrelationId = Guid.NewGuid();
        }

        public Guid CorrelationId { get; }
    }
}
