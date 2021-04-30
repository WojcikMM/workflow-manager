using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Statuses.Commands;
using WorkflowManagerMonolith.Application.Statuses.DTOs;
using WorkflowManagerMonolith.Application.Statuses.Queries;

namespace WorkflowManagerMonolith.Application.Statuses
{
    public interface IStatusesService
    {
        public Task<IEnumerable<StatusDto>> BrowseStatusesAsync(GetStatusesQuery query);
        public Task<StatusDto> GetStatusById(Guid id);
        public Task CreateStatusAsync(CreateStatusCommand command);
        public Task UpdateStatusAsync(Guid Id, UpdateStatusCommand command);
    }
}
