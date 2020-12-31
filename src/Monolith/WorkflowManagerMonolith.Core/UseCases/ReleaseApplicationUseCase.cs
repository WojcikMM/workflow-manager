using System;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Core.Abstractions.UseCases;
using WorkflowManagerMonolith.Core.Commands;
using WorkflowManagerMonolith.Core.Repositories;

namespace WorkflowManagerMonolith.Core.UseCases
{
    public class ReleaseApplicationUseCase : IReleaseApplicationUseCase
    {
        private readonly IApplicationRepository applicationRepository;

        public ReleaseApplicationUseCase(IApplicationRepository applicationRepository)
        {
            this.applicationRepository = applicationRepository;
        }

        public async Task HandleAsync(ReleaseApplicationCommand data)
        {
            var application = await applicationRepository.GetAsync(data.ApplicationId);
            if (application == null)
            {
                throw new Exception("Application with given id not found");
            }
            application.ReleaseHandling();
            await applicationRepository.UpdateAsync(application);
        }
    }
}
