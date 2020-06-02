using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.OperationsStorage.Api.Dto;

namespace WorkflowManager.OperationsStorage.Api.Services
{
    public class OperationStorage : IOperationsStorage
    {
        private readonly IDistributedCache _cache;

        public OperationStorage(IDistributedCache cache) => _cache = cache;

        public async Task<OperationDto> GetAsync(Guid id)
        {
            var operation = await _cache.GetStringAsync(id.ToString());
            return string.IsNullOrWhiteSpace(operation) ? null : JsonConvert.DeserializeObject<OperationDto>(operation);
        }

        public async Task SetAsync(IEvent @event)
        {
            var status = @event is IRejectedEvent ? "Rejected" : @event is ICompleteEvent ? "Complete" : "Pending";
            await _cache.SetStringAsync(@event.CorrelationId.ToString(),
                JsonConvert.SerializeObject(new OperationDto
                {
                    AggregateId = @event.AggregateId,
                    Status = status
                }),
                new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20),
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
        }
    }
}
