using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Applications;
using WorkflowManagerMonolith.Application.Applications.DTOs;
using WorkflowManagerMonolith.Core.Repositories;

namespace WorkflowManagerMonolith.Infrastructure.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IMapper mapper;
        private readonly IApplicationRepository applicationRepository;

        public ApplicationService(IMapper mapper, IApplicationRepository applicationRepository)
        {
            this.mapper = mapper;
            this.applicationRepository = applicationRepository;
        }
        public async Task<IEnumerable<ApplicationDto>> BrowseApplicationsAsync(GetApplicationsQuery query)
        {
            var applications = await applicationRepository.GetAllAsync();
            return mapper.Map<IEnumerable<ApplicationDto>>(applications);
        }

        public async Task CreateApplicationAsync(CreateApplicationCommand command)
        {
            await applicationRepository.CreateAsync(new Core.Domain.ApplicationEntity(command.ApplicationId));
        }

        public Task<ApplicationDto> GetApplicationByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateApplicationAsync(UpdateApplicationCommand command)
        {
            var application = await applicationRepository.GetAsync(command.ApplicationId);
            

            await applicationRepository.UpdateAsync(application);
        }
    }
}
