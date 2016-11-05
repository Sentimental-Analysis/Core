using System;
using System.Threading.Tasks;

namespace Core.Cache.Interfaces
{
    public interface ICacheService
    {
        T GetOrStore<T>(string key, Func<T> func, TimeSpan timeForCache);
        Task<T> GetOrStoreAsync<T>(string key, Func<Task<T>> func, TimeSpan timeForCache);
        string GenerateKey(string name, params string[] args);
        void Clear(string key);
    }
}