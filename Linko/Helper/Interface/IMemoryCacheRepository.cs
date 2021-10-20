namespace Linko.Helper
{
    public interface IMemoryCacheRepository
    {
        public void Set(string key, object data);
        public object Get(string key);
        public (bool exist, object data) TryGetValue(string key);
        public void Remove(string key);
    }
}
