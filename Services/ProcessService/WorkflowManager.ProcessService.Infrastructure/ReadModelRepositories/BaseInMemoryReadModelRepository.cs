using System;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Template.ReadModel;
using System.Collections.Generic;

namespace WorkflowManager.ProcessService.Infrastructure.ReadModelRepositories
{
    public abstract class BaseInMemoryReadModelRepository<TReadModel> : IReadModelRepository<TReadModel> where TReadModel : IReadModel, new()
    {
        private readonly ISet<TReadModel> _models;

        public BaseInMemoryReadModelRepository()
        {
            _models = new HashSet<TReadModel>();
        }

        public async Task Add(TReadModel model)
        {
            _models.Add(model);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<TReadModel>> GetAll()
        {
            return await Task.FromResult(_models.ToList());
        }

        public async Task<TReadModel> GetById(Guid id)
        {
            var process = _models.FirstOrDefault(m => m.Id == id);
            if (process is null)
            {
                throw new KeyNotFoundException("Process with given id is not exists.");
            }
            return await Task.FromResult(process);
        }

        public async Task Remove(Guid id)
        {
            var process = await GetById(id);

            _models.Remove(process);
        }

        public async Task Update(TReadModel model)
        {
            var readModel = await GetById(model.Id);
            ModelUpdateMethod(readModel, model);
        }

        protected abstract void ModelUpdateMethod(TReadModel currentReadModel, TReadModel incomingReadModel);
    }
}
