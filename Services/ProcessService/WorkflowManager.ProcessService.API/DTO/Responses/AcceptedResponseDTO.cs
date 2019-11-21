using System;

namespace WorkflowManager.ProcessService.API.DTO.Responses
{
    public class AcceptedResponseDTO : CorrelationIdResponseDTO
    {
        public AcceptedResponseDTO() : base()
        {
            this.ProductId = Guid.NewGuid();
        }
        public Guid ProductId { get; }
    }
}
