using System;
using RestEase;
using System.Threading.Tasks;
using WorkflowManager.Gateway.API.DTOs;

namespace WorkflowManager.Gateway.API.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.ToString)]
    public interface IOperationsService
    {
        [AllowAnyStatusCode]
        [Get("operations/{operationId}")]
        Task<Response<OperationDTO>> GetAsync([Path] Guid operationId);
    }
}
