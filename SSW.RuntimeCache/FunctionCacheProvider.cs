using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SSW.RuntimeCache
{
    public class FunctionCacheProvider : ICacheProvider
    {
        private readonly IObjectCache _objectCache;

        public IObjectCache Cache 
        {
            get { return _objectCache; }
        }

        public FunctionCacheProvider(IObjectCache objectCache)
        {
            _objectCache = objectCache;
        }

        public T ExecuteWithCache<T>(string key, Func<T> func, DateTimeOffset expiryOffset, params object[] cacheByParams)
        {
            if (cacheByParams != null && cacheByParams.Any())
            {
                var hash = JsonConvert.SerializeObject(cacheByParams).GetHashCode();
                key += hash;
            }

            var cachedResult = _objectCache.Get(key);

            if (cachedResult != null)
            {
                if (cachedResult is T)
                {
                    return (T) cachedResult;
                }

                return (T) Convert.ChangeType(cachedResult, typeof (T));
            }

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = expiryOffset;

            var result = func();
            _objectCache.Add(key, result, policy);

            return result;
        }
    }
}
