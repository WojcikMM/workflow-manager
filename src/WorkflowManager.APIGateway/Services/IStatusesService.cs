using RestEase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManagerGateway.DTOs;

namespace WorkflowManagerGateway.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IStatusesService
    {
        [AllowAnyStatusCode]
        [Get("statuses")]
        Task<IEnumerable<StatusDTO>> BrowseAsync([Query]string name);

        [AllowAnyStatusCode]
        [Get("statuses/{id}")]
        Task<StatusDTO> GetAsync([Path]Guid id);
    }
}
