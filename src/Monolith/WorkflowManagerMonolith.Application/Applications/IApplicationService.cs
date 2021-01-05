using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Applications.DTOs;
using WorkflowManagerMonolith.Application.Transactions.DTOs;

namespace WorkflowManagerMonolith.Application.Applications
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDto>> BrowseApplicationsAsync(GetApplicationsQuery query);
        Task<ApplicationDto> GetApplicationByIdAsync(Guid id);
        Task<IEnumerable<TransactionDto>> GetAllowedTransactionsAsync(Guid applicationId);
        Task CreateApplicationAsync(CreateApplicationCommand command);
        Task ApplyTransaction(ApplyTransactionCommand command);
        Task AssignUserHandling(AssignUserHandlingCommand command);
        Task ReleaseUserHandling(ReleaseUserHandlingCommand command);
    }
}
