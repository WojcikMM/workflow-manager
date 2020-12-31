using System;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Core.Abstractions.UseCases;
using WorkflowManagerMonolith.Core.Commands;
using WorkflowManagerMonolith.Core.Repositories;

namespace WorkflowManagerMonolith.Core.UseCases
{
    public class AssignApplicationToUserUseCase : IAssignApplicationToUserUseCase
    {
        private readonly IApplicationRepository applicationRepository;

        public AssignApplicationToUserUseCase(IApplicationRepository applicationRepository)
        {
            this.applicationRepository = applicationRepository;
        }

        public async Task HandleAsync(AssignApplicationToUserCommand command)
        {
            var application = await applicationRepository.GetAsync(command.ApplicationId);
            if (application == null)
            {
                throw new Exception("Application with given id not found");
            }

            application.AssingnToHandling(command.UserId);
            await applicationRepository.UpdateAsync(application);
        }
    }
}
