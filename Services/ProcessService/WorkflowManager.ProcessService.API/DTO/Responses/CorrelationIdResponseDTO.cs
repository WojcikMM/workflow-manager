using System;

namespace WorkflowManager.ProcessService.API.DTO.Responses
{
    public class CorrelationIdResponseDTO
    {
        public CorrelationIdResponseDTO()
        {
            this.CorrelationId = Guid.NewGuid();
        }

        public Guid CorrelationId { get; }
    }
}
