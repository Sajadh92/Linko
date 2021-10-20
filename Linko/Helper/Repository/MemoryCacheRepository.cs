using Microsoft.Extensions.Caching.Memory;

namespace Linko.Helper
{
    public class MemoryCacheRepository : IMemoryCacheRepository, IRegisterSingleton
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheRepository(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Set(string key, object data)
        {
            _cache.Set(key, data);
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public (bool exist, object data) TryGetValue(string key)
        {
            bool exist = _cache.TryGetValue(key, out object data);

            return (exist, data);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
