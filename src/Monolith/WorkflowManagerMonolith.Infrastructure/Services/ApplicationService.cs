using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Applications;
using WorkflowManagerMonolith.Application.Applications.Commands;
using WorkflowManagerMonolith.Application.Applications.DTOs;
using WorkflowManagerMonolith.Application.Applications.Queries;
using WorkflowManagerMonolith.Application.Transactions.DTOs;
using WorkflowManagerMonolith.Core.Domain;
using WorkflowManagerMonolith.Core.Exceptions;
using WorkflowManagerMonolith.Core.Repositories;

namespace WorkflowManagerMonolith.Infrastructure.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IMapper mapper;
        private readonly IApplicationRepository applicationRepository;
        private readonly ITransactionRepository transactionRepository;
        private readonly IStatusesRepository statusesRepository;

        public ApplicationService(IMapper mapper,
            IApplicationRepository applicationRepository,
            ITransactionRepository transactionRepository,
            IStatusesRepository statusesRepository)
        {
            this.mapper = mapper;
            this.applicationRepository = applicationRepository;
            this.transactionRepository = transactionRepository;
            this.statusesRepository = statusesRepository;
        }

        public async Task ApplyTransaction(ApplyTransactionCommand command)
        {
            var application = await GetApplication(command.ApplicationId);

            var transaction = await transactionRepository.GetAsync(command.TransactionId);

            if (transaction == null || command.TransactionId == Guid.Empty)
            {
                throw new AggregateIllegalLogicException("Invalid transaction Id");
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
            var applicationsDtos = mapper.Map<IEnumerable<ApplicationDto>>(applications);

            var results = applicationsDtos.Select(async app =>
            {
                var status = await statusesRepository.GetAsync(app.StatusId);
                if (status != null)
                {
                    app.StatusName = status.Name;
                }
                return app;
            });

            return await Task.WhenAll(results);
        }

        public async Task CreateApplicationAsync(CreateApplicationCommand command)
        {
            if (command.ApplicationId == Guid.Empty)
            {
                throw new AggregateValidationException("Invalid application Id");
            }

            if (command.RegistrationUser == Guid.Empty)
            {
                throw new AggregateValidationException("Invalid user Id");
            }

            if (command.InitialTransactionId == Guid.Empty)
            {
                throw new AggregateValidationException("Invalid transaction Id");
            }

            var application = await applicationRepository.GetAsync(command.ApplicationId);
            if (application != null)
            {
                throw new AggregateIllegalLogicException("Cannot create application with given id. Application exists.");
            }

            var transaction = await transactionRepository.GetAsync(command.InitialTransactionId);
            if (transaction == null)
            {
                throw new AggregateValidationException("Transaction with given Id not exists.");
            }

            application = new ApplicationEntity(command.ApplicationId);


            application.ApplyTransaction(transaction, command.RegistrationUser);


            await applicationRepository.CreateAsync(application);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllowedTransactionsAsync(Guid applicationId)
        {
            var application = await GetApplication(applicationId);

            var avaliableTransactions = application.StatusId.HasValue ?
                await transactionRepository.GetByIncomingStatusAsync(application.StatusId.Value) :
                await transactionRepository.GetStartingTransactions();

            return mapper.Map<IEnumerable<TransactionDto>>(avaliableTransactions);
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
                throw new AggregateValidationException("Invalid application Id");
            }

            var application = await applicationRepository.GetAsync(applicationId);
            if (application == null)
            {
                throw new AggregateNotFoundException("Application with given Id not exists.");
            }

            return application;
        }
    }
}
