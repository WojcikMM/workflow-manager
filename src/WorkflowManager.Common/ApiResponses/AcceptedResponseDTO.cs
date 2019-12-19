using System;

namespace WorkflowManager.Common.ApiResponses
{
    public class AcceptedResponseDTO : CorrelationIdResponseDTO
    {
        public AcceptedResponseDTO() : base()
        {
            ProductId = Guid.NewGuid();
        }

        public AcceptedResponseDTO(Guid productId) : base()
        {
            ProductId = productId;
        }
        public Guid ProductId { get; }
    }
}
