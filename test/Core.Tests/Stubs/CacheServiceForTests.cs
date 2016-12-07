using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Cache.Interfaces;

namespace Core.Tests.Stubs
{
    public class CacheServiceForTests : ICacheService
    {
        private readonly Dictionary<string, dynamic> _data;

        public CacheServiceForTests()
        {
            _data = new Dictionary<string, object>();
        }

        public T GetOrStore<T>(string key, Func<T> func, TimeSpan timeForCache)
        {
            dynamic data;
            if (_data.TryGetValue(key, out data))
            {
                return data;
            }
        }

        public Task<T> GetOrStoreAsync<T>(string key, Func<Task<T>> func, TimeSpan timeForCache)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string key)
        {
            dynamic data;
            return _data.TryGetValue(key, out data);
        }

        public void Clear(string key)
        {
            _data.Remove(key);
        }
    }
}