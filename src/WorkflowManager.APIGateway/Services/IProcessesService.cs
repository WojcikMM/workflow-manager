using RestEase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManagerGateway.DTOs;

namespace WorkflowManagerGateway.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IProcessesService
    {
        [AllowAnyStatusCode]
        [Get("processes")]
        Task<IEnumerable<ProcessDTO>> BrowseAsync([Query]string name = "");

        [AllowAnyStatusCode]
        [Get("processes/{id}")]
        Task<ProcessDTO> GetAsync([Path]Guid id);
    }
}
