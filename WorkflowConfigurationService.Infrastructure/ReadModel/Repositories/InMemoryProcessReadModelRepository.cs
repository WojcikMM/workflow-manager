using System;
using System.Threading.Tasks;
using CQRS.Template.ReadModel;
using System.Collections.Generic;
using WorkflowConfigurationService.Infrastructure.ReadModel.Models;
using System.Linq;

namespace WorkflowConfigurationService.Infrastructure.ReadModel.Repositories
{
    public class InMemoryProcessReadModelRepository : IReadModelRepository<ProcessReadModel>
    {
        private readonly ISet<ProcessReadModel> _models;

        public InMemoryProcessReadModelRepository()
        {
            _models = new HashSet<ProcessReadModel>();
        }

        public async Task Add(ProcessReadModel model)
        {
            _models.Add(model);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<ProcessReadModel>> GetAll()
        {
            return await Task.FromResult(_models.ToList());
        }

        public async Task<ProcessReadModel> GetById(Guid id, bool failIfNull = false)
        {
            var process = _models.FirstOrDefault(m => m.Id == id);
            if (failIfNull && process is null)
            {
                throw new KeyNotFoundException("Process with given id is not exists.");
            }
            return await Task.FromResult(process);
        }

        public async Task Remove(Guid id)
        {
            var process = await GetById(id, true);

            _models.Remove(process);
        }

        public async Task Update(ProcessReadModel model)
        {
            var process = await GetById(model.Id,true);
            process.Name = model.Name;
        }
    }
}
