using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CQRS.Template.ReadModel;
using WorkflowManager.Common.Exceptions;
using WorkflowManager.ProcessService.ReadModel.ReadDatabase;
using System.Linq;

namespace WorkflowManager.ProcessService.ReadModel
{
    public class ProcessReadModelRepository : IReadModelRepository<ProcessModel>
    {
        private readonly ProcessesContext _context;

        public ProcessReadModelRepository(ProcessesContext context) => _context = context;

        public async Task AddAsync(ProcessModel model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<ProcessModel>> SearchAsync(string query)
        {
            var processes = _context.Processes.Select(m => m);
            if (!string.IsNullOrWhiteSpace(query))
            {
                processes = processes
                    .Where(m=> m.Name.ToLower().Contains(query.ToLower()));
            }

            return await processes.ToListAsync();
        }

        public async Task<IEnumerable<ProcessModel>> GetAllAsync() =>
           await SearchAsync(null);

        public async Task<ProcessModel> GetByIdAsync(Guid id)
        {
            var process = await _context.Processes.FindAsync(id);
            if (process is null)
            {
                throw new ReadModelNotFoundException($"Cannot find model with given id. ({id})");
            }
            return process;
        }

        public async Task RemoveAsync(Guid id)
        {
            var process = await GetByIdAsync(id);
            _context.Remove(process);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(ProcessModel model)
        {
            var process = await GetByIdAsync(model.Id);
            process.Name = model.Name;
            process.Version = model.Version;
            process.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
