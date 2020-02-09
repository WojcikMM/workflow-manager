﻿using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
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

        public async Task SetAsync(Guid id, OperationDto @event)
        {
            await _cache.SetStringAsync(id.ToString(),
                JsonConvert.SerializeObject(@event),
                new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20),
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
        }
    }
}