using System;
using System.Threading.Tasks;
using Core.Cache.Interfaces;


namespace Core.Tests.Mocks
{
    public class MemoryCacheFakeService : ICacheService
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T GetOrStore<T>(string key, Func<T> func, TimeSpan timeForCache)
        {
            return func();
        }

        public Task<T> GetOrStoreAsync<T>(string key, Func<Task<T>> func, TimeSpan timeForCache)
        {
            throw new NotImplementedException();
        }

        public void Clear(string key)
        {
            throw new NotImplementedException();
        }
    }
}