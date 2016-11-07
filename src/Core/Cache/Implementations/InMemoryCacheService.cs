using System;
using System.Text;
using System.Threading.Tasks;
using Core.Cache.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Cache.Implementations
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T GetOrStore<T>(string key, Func<T> func, TimeSpan timeForCache)
        {
            T result;
            if (!_memoryCache.TryGetValue(key, out result))
            {
                result = func();
                _memoryCache.Set(key, result, timeForCache);
            }
            return result;
        }

        public async Task<T> GetOrStoreAsync<T>(string key, Func<Task<T>> func, TimeSpan timeForCache)
        {
            T result;
            if (!_memoryCache.TryGetValue(key, out result))
            {
                result = await func();
                _memoryCache.Set(key, await func(), timeForCache);
            }
            return result;
        }

        public void Clear(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}