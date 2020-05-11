using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;

namespace Mvc2CacheDemo.Services
{
    public interface IMyService
    {
        string GetName();
        int GetAnother(int b);
    }

    class MyService : IMyService
    {
        public string GetName()
        {
            return "Stefan";
        }

        public int GetAnother(int b)
        {
            //Fake fetching from slow service...
            System.Threading.Thread.Sleep(1000);

            return b + 10;
        }
    }
    class CachedMyService : IMyService
    {
        private readonly IMyService _inner;
        private readonly IMemoryCache _cache;

        public CachedMyService(IMyService inner, IMemoryCache cache)
        {
            _inner = inner;
            _cache = cache;
        }
        public string GetName()
        {
            return _inner.GetName();
        }

        public int GetAnother(int b)
        {
            return _cache.GetOrCreate($"MyService-{b}", e => _inner.GetAnother(b));
        }
    }
}