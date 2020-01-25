using System;
using RestEase;
using System.Threading.Tasks;
using System.Collections.Generic;
using WorkflowManager.Gateway.API.DTOs;

namespace WorkflowManager.Gateway.API.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.ToString)]
    public interface IProcessesService
    {
        [AllowAnyStatusCode]
        [Get("processes")]
        Task<Response<IEnumerable<ProcessDTO>>> BrowseAsync([Query]string name);

        [AllowAnyStatusCode]
        [Get("processes/{id}")]
        Task<Response<ProcessDTO>> GetAsync([Path]Guid id);
    }
}
