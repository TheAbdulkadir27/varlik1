using Microsoft.Extensions.Caching.Memory;
using Varlık_ZimmetDepoYonetimi.UI.Services.Interfaces;

namespace Varlık_ZimmetDepoYonetimi.UI.Services.Implementations
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _defaultExpirationTime = TimeSpan.FromSeconds(5);

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T? Get<T>(string key) => _memoryCache.Get<T>(key);

        public void Set<T>(string key, T value, TimeSpan? expirationTime = null)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime ?? _defaultExpirationTime
            };
            _memoryCache.Set(key, value, options);
        }

        public void Remove(string key) => _memoryCache.Remove(key);

        public bool Exists(string key) => _memoryCache.TryGetValue(key, out _);
    }
}