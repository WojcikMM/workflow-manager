using WorkflowManager.CQRS.ReadModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase;
using System.Security.Cryptography.X509Certificates;

namespace WorkflowManager.ConfigurationService.ReadModel.Repositories
{
    public class ProcessReadModelRepository : IReadModelRepository<ProcessModel>
    {
        private readonly ConfigurationDbContext _context;

        public ProcessReadModelRepository(ConfigurationDbContext context) => _context = context;

        public async Task AddAsync(ProcessModel model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<ProcessModel>> SearchAsync(string query)
        {
            IQueryable<ProcessModel> processes = _context.Processes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
                processes = processes
                    .Where(m => m.Name.ToLower().Contains(query.ToLower()));
            }

            return await processes.ToListAsync();
        }

        public async Task<IEnumerable<ProcessModel>> GetAllAsync() =>
           await SearchAsync(null);

        public async Task<ProcessModel> GetByIdAsync(Guid id) =>
            await _context.Processes.FindAsync(id);

        public async Task RemoveAsync(Guid id)
        {
            ProcessModel process = await GetByIdAsync(id);
            _context.Remove(process);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProcessModel model)
        {
            ProcessModel process = await GetByIdAsync(model.Id);
            process.Name = model.Name;
            process.Version = model.Version;
            process.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Guid id)
        {
            return _context.Processes.AnyAsync(x => x.Id == id);
        }
    }
}
