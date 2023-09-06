using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CarSales.Core.Extensions
{
    public static class DistributedCacheExtension
    {
        private static HashSet<string> keys = new HashSet<string>();

        public static HashSet<string> Keys
        {
            get { return keys; }
        }

        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = unusedExpireTime;

            var jsonData = JsonSerializer.Serialize(data);
            keys.Add(recordId);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {

            var jsonData = await cache.GetStringAsync(recordId);
            if (jsonData == null)
            {
                return default(T);
            }
            var data = JsonSerializer.Deserialize<T>(jsonData);

            return data;
        }

        public static async Task ClearCacheAsync(this IDistributedCache cache)
        {
            foreach (var key in keys)
            {
                await cache.RemoveAsync(key);
            }
        }
    }
}
