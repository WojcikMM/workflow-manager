using System;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Core.Abstractions.UseCases;
using WorkflowManagerMonolith.Core.Commands;
using WorkflowManagerMonolith.Core.Domain;
using WorkflowManagerMonolith.Core.Repositories;

namespace WorkflowManagerMonolith.Core.UseCases
{
    public class GetApplicationUseCase : IGetApplicationUseCase
    {
        private readonly IApplicationRepository ApplicationRepository;

        public GetApplicationUseCase(IApplicationRepository applicationRepository)
        {
            ApplicationRepository = applicationRepository;
        }
        public async Task<ApplicationEntity> HandleAsync(GetApplicationCommand command)
        {
            var application = await ApplicationRepository.GetAsync(command.ApplicationId);
            if (application is null)
            {
                throw new NullReferenceException("Application not exists");
            }
            return application;
        }
    }
}
