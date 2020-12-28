using System;
using System.Collections.Generic;
using System.Text;
using WorkflowManagerMonolith.Core.Entities;
using WorkflowManagerMonolith.Core.Repository;

namespace WorkflowManagerMonolith.Core.UseCases
{
    internal interface IUseCaseHandler<in TRequest, out TResponse>
    {
        TResponse Handle(TRequest data);
    }


    public class GetApplicationUseCase : IUseCaseHandler<Guid, ApplicationEntity>
    {
        private readonly IRepository<ApplicationEntity> repository;

        public GetApplicationUseCase(IRepository<ApplicationEntity> repository)
        {
            this.repository = repository;
        }
        public ApplicationEntity Handle(Guid applicationId)
        {
            var application = repository.Get(applicationId);
            if(application is null)
            {
                throw new NullReferenceException("Application not exists");
            }
            return application;
        }
    }



    public class HandleTransactionUseCase : IUseCaseHandler<>
    {
    }
}
