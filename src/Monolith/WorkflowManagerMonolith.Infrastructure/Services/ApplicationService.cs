using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Applications;
using WorkflowManagerMonolith.Application.Applications.DTOs;
using WorkflowManagerMonolith.Core.Domain;
using WorkflowManagerMonolith.Core.Repositories;

namespace WorkflowManagerMonolith.Infrastructure.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IMapper mapper;
        private readonly IApplicationRepository applicationRepository;
        private readonly ITransactionRepository transactionRepository;

        public ApplicationService(IMapper mapper, IApplicationRepository applicationRepository, ITransactionRepository transactionRepository)
        {
            this.mapper = mapper;
            this.applicationRepository = applicationRepository;
            this.transactionRepository = transactionRepository;
        }

        public async Task ApplyTransaction(ApplyTransactionCommand command)
        {
            var application = await GetApplication(command.ApplicationId);

            var transaction = await transactionRepository.GetAsync(command.TransactionId);

            if (transaction == null || command.TransactionId == Guid.Empty)
            {
                throw new Exception("Invalid transaction Id");
            }

            if (command.UserId == Guid.Empty)
            {
                throw new Exception("Invalid User Id");
            }

            application.ApplyTransaction(transaction, command.UserId);
        }

        public async Task AssignUserHandling(AssignUserHandlingCommand command)
        {
            var application = await GetApplication(command.ApplicationId);

            application.AssingnToHandling(command.UserId);
        }

        public async Task<IEnumerable<ApplicationDto>> BrowseApplicationsAsync(GetApplicationsQuery query)
        {
            var applications = await applicationRepository.GetAllAsync();
            return mapper.Map<IEnumerable<ApplicationDto>>(applications);
        }

        public async Task CreateApplicationAsync(CreateApplicationCommand command)
        {
            if (command.ApplicationId == Guid.Empty)
            {
                throw new Exception("Invalid application Id");
            }
            await applicationRepository.CreateAsync(new ApplicationEntity(command.ApplicationId));
        }

        public async Task<ApplicationDto> GetApplicationByIdAsync(Guid Id)
        {
            var application = await GetApplication(Id);

            return mapper.Map<ApplicationDto>(application);
        }

        public async Task ReleaseUserHandling(ReleaseUserHandlingCommand command)
        {
            var application = await GetApplication(command.ApplicationId);

            application.ReleaseHandling();
        }

        private async Task<ApplicationEntity> GetApplication(Guid applicationId)
        {
            if (applicationId == Guid.Empty)
            {
                throw new Exception("Invalid application Id");
            }

            var application = await applicationRepository.GetAsync(applicationId);
            if (application == null)
            {
                throw new Exception("Application with given Id not exists.");
            }

            return application;
        }
    }
}
