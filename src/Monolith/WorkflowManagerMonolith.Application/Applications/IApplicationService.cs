using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Applications.DTOs;

namespace WorkflowManagerMonolith.Application.Applications
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDto>> BrowseApplicationsAsync(GetApplicationsQuery query);
        Task<ApplicationDto> GetApplicationByIdAsync(Guid id);
        Task CreateApplicationAsync(CreateApplicationCommand command);
        Task ApplyTransaction(ApplyTransactionCommand command);
        Task AssignUserHandling(AssignUserHandlingCommand command);
        Task ReleaseUserHandling(ReleaseUserHandlingCommand command);
    }
}
