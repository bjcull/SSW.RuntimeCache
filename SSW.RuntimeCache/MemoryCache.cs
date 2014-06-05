using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SSW.RuntimeCache
{
    public class MemoryCache : System.Runtime.Caching.MemoryCache, IObjectCache 
    {
        public MemoryCache(string name, NameValueCollection config = null) : base(name, config)
        {
        }
    }
}
