using System;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Core.Abstractions.UseCases;
using WorkflowManagerMonolith.Core.Commands;
using WorkflowManagerMonolith.Core.Repositories;

namespace WorkflowManagerMonolith.Core.UseCases
{
    public class HandleTransactionUseCase : IHandleTransactionUseCase
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IApplicationRepository applicationRepository;

        public HandleTransactionUseCase(ITransactionRepository transactionRepository, IApplicationRepository applicationRepository)
        {
            this.transactionRepository = transactionRepository;
            this.applicationRepository = applicationRepository;
        }

        public async Task HandleAsync(HandleTransactionCommand command)
        {
            var application = await applicationRepository.GetAsync(command.ApplicationId);
            if (application == null)
            {
                throw new Exception("Application with given id not found.");
            }

            var transaction = await transactionRepository.GetAsync(command.TransactionId);
            if (transaction == null)
            {
                throw new Exception("Transaction with given id not found.");
            }

            application.ApplyTransaction(transaction, command.UserId);

            await applicationRepository.UpdateAsync(application);
        }
    }
}
