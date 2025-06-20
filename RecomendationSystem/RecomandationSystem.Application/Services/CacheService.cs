using Microsoft.Extensions.Caching.Distributed;
using RecomandationSystem.Application.Interfaces;
using System.Text.Json;

namespace RecomandationSystem.Application.Services
{
    internal sealed class CacheService(IDistributedCache cache) : ICacheService
    {
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken)
        {
            var obj = await cache.GetAsync(key, cancellationToken);

            if (obj is null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(obj);
        }

        public async Task SetAsync<T>(string key, T obj, TimeSpan? expiration = null,  CancellationToken cancellationToken = default)
        {
            var options = new DistributedCacheEntryOptions();

            if(expiration is not null)
            {
                options.AbsoluteExpirationRelativeToNow = expiration;
            }

            var serializedObj = JsonSerializer.Serialize(obj);

            await cache.SetStringAsync(
                key,
                serializedObj,
                options,
                cancellationToken);
        }
    }
}
