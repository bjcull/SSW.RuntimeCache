using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.RuntimeCache
{
    public interface ICacheProvider
    {
        IObjectCache Cache { get; }

        T ExecuteWithCache<T>(string key, Func<T> func, DateTimeOffset expiryOffset, params object[] cacheByParams);
    }
}
