using System;

namespace WorkflowManager.ProcessService.API.DTO.Responses
{
    public class AcceptedResponseDTO : CorrelationIdResponseDTO
    {
        public AcceptedResponseDTO() : base()
        {
            this.ProductId = Guid.NewGuid();
        }

        public AcceptedResponseDTO(Guid productId) : base()
        {
            this.ProductId = productId;
        }
        public Guid ProductId { get; }
    }
}
