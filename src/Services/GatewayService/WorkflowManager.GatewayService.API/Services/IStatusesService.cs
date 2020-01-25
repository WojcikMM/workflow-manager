using System;
using RestEase;
using System.Threading.Tasks;
using System.Collections.Generic;
using WorkflowManager.Gateway.API.DTOs;

namespace WorkflowManager.Gateway.API.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.ToString)]
    public interface IStatusesService
    {
        [AllowAnyStatusCode]
        [Get("statuses")]
        Task<Response<IEnumerable<StatusDTO>>> BrowseAsync([Query]string name);

        [AllowAnyStatusCode]
        [Get("statuses/{id}")]
        Task<Response<StatusDTO>> GetAsync([Path]Guid id);
    }
}
