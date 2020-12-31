using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Core.Domain;
using WorkflowManagerMonolith.Core.Repositories;
using WorkflowManagerMonolith.Application.Models;
using WorkflowManagerMonolith.Application.UnitOfWork;

namespace WorkflowManagerMonolith.Application.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ApplicationRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateAsync(ApplicationEntity entity)
        {
            var application = mapper.Map<ApplicationModel>(entity);
            unitOfWork.Applications.Add(application);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<ApplicationEntity> GetAsync(Guid id)
        {
            var applicationModel = GetModelById(id);
            var application = mapper.Map<ApplicationEntity>(applicationModel);
            return await Task.FromResult(application);

        }

        public async Task UpdateAsync(ApplicationEntity entity)
        {
            var applicationModel = GetModelById(entity.Id);
            applicationModel.StatusId = entity.StatusId;
            applicationModel.TransactionItems = mapper.Map<IEnumerable<TransactionItemModel>>(entity.TransactionItems);
            applicationModel.CreatedAt = entity.CreatedAt;
            applicationModel.UpdatedAt = entity.UpdatedAt;
            applicationModel.UserId = entity.AssignedUserId;

            await unitOfWork.SaveChangesAsync();
        }

        private ApplicationModel GetModelById(Guid id)
        {
            return unitOfWork.Applications.FirstOrDefault(transaction => transaction.Id == id);
        }
    }
}
