using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.OperationsService.API;
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
            var status = GetEventType(@event).ToString();
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

        private EventType GetEventType(IEvent @event)
        {
            return @event switch
            {
                IRejectedEvent _ => EventType.REJECTED,
                ICompleteEvent _ => EventType.COMPLETE,
                _ => EventType.PENDING
            };
        }
    }
}
