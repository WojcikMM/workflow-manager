using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using CQRS.Template.ReadModel;
using WorkflowManager.Common.Exceptions;
using WorkflowManager.ProcessService.ReadModel.ReadDatabase;

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

        public async Task<IEnumerable<ProcessModel>> GetAll() =>
            await _context.Processes.ToListAsync();

        public async Task<ProcessModel> GetByIdAsync(Guid id)
        {
            var process = await _context.Processes.FindAsync(id);
            if (process is null)
            {
                throw new ReadModelNotFoundException($"Cannot find model with given id. ({id})");
            }
            return process;
        }

        public async Task Remove(Guid id)
        {
            var process = await GetByIdAsync(id);
            _context.Remove(process);
            await _context.SaveChangesAsync();

        }

        public async Task Update(ProcessModel model)
        {
            var process = await GetByIdAsync(model.Id);
            process.Name = model.Name;
            process.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
