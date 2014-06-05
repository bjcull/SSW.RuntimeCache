using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SSW.RuntimeCache
{
    public interface IObjectCache : IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        /// <summary>
        /// When overridden in a derived class, checks whether the cache entry already exists in the cache.
        /// </summary>
        /// 
        /// <returns>
        /// true if the cache contains a cache entry with the same key value as <paramref name="key"/>; otherwise, false.
        /// </returns>
        /// <param name="key">A unique identifier for the cache entry. </param><param name="regionName">Optional. A named region in the cache where the cache can be found, if regions are implemented. The default value for the optional parameter is null.</param>
        bool Contains(string key, string regionName = null);

        /// <summary>
        /// When overridden in a derived class, inserts a cache entry into the cache without overwriting any existing cache entry.
        /// </summary>
        /// 
        /// <returns>
        /// true if insertion succeeded, or false if there is an already an entry in the cache that has the same key as <paramref name="key"/>.
        /// </returns>
        /// <param name="key">A unique identifier for the cache entry.</param><param name="value">The object to insert. </param><param name="absoluteExpiration">The fixed date and time at which the cache entry will expire. This parameter is required when the <see cref="Overload:System.Runtime.Caching.ObjectCache.Add"/> method is called.</param><param name="regionName">Optional. A named region in the cache to which the cache entry can be added, if regions are implemented. Because regions are not implemented in .NET Framework 4, the default value is null.</param>
        bool Add(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null);

        /// <summary>
        /// When overridden in a derived class, tries to insert a cache entry into the cache as a <see cref="T:System.Runtime.Caching.CacheItem"/> instance, and adds details about how the entry should be evicted.
        /// </summary>
        /// 
        /// <returns>
        /// true if insertion succeeded, or false if there is an already an entry in the cache that has the same key as <paramref name="item"/>.
        /// </returns>
        /// <param name="item">The object to add.</param><param name="policy">An object that contains eviction details for the cache entry. This object provides more options for eviction than a simple absolute expiration. </param>
        bool Add(CacheItem item, CacheItemPolicy policy);

        /// <summary>
        /// When overridden in a derived class, inserts a cache entry into the cache, specifying information about how the entry will be evicted.
        /// </summary>
        /// 
        /// <returns>
        /// true if the insertion try succeeds, or false if there is an already an entry in the cache with the same key as <paramref name="key"/>.
        /// </returns>
        /// <param name="key">A unique identifier for the cache entry. </param><param name="value">The object to insert. </param><param name="policy">An object that contains eviction details for the cache entry. This object provides more options for eviction than a simple absolute expiration. </param><param name="regionName">Optional. A named region in the cache to which the cache entry can be added, if regions are implemented. The default value for the optional parameter is null.</param>
        bool Add(string key, object value, CacheItemPolicy policy, string regionName = null);

        /// <summary>
        /// When overridden in a derived class, inserts a cache entry into the cache, by using a key, an object for the cache entry, an absolute expiration value, and an optional region to add the cache into.
        /// </summary>
        /// 
        /// <returns>
        /// If a cache entry with the same key exists, the specified cache entry's value; otherwise, null.
        /// </returns>
        /// <param name="key">A unique identifier for the cache entry. </param><param name="value">The object to insert. </param><param name="absoluteExpiration">The fixed date and time at which the cache entry will expire. </param><param name="regionName">Optional. A named region in the cache to which the cache entry can be added, if regions are implemented. The default value for the optional parameter is null.</param>
        object AddOrGetExisting(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null);

        /// <summary>
        /// When overridden in a derived class, inserts the specified <see cref="T:System.Runtime.Caching.CacheItem"/> object into the cache, specifying information about how the entry will be evicted.
        /// </summary>
        /// 
        /// <returns>
        /// If a cache entry with the same key exists, the specified cache entry; otherwise, null.
        /// </returns>
        /// <param name="value">The object to insert. </param><param name="policy">An object that contains eviction details for the cache entry. This object provides more options for eviction than a simple absolute expiration.</param>
        CacheItem AddOrGetExisting(CacheItem value, CacheItemPolicy policy);

        /// <summary>
        /// When overridden in a derived class, inserts a cache entry into the cache, specifying a key and a value for the cache entry, and information about how the entry will be evicted.
        /// </summary>
        /// 
        /// <returns>
        /// If a cache entry with the same key exists, the specified cache entry's value; otherwise, null.
        /// </returns>
        /// <param name="key">A unique identifier for the cache entry. </param><param name="value">The object to insert.</param><param name="policy">An object that contains eviction details for the cache entry. This object provides more options for eviction than a simple absolute expiration. </param><param name="regionName">Optional. A named region in the cache to which the cache entry can be added, if regions are implemented. The default value for the optional parameter is null.</param>
        object AddOrGetExisting(string key, object value, CacheItemPolicy policy, string regionName = null);

        /// <summary>
        /// When overridden in a derived class, gets the specified cache entry from the cache as an object.
        /// </summary>
        /// 
        /// <returns>
        /// The cache entry that is identified by <paramref name="key"/>.
        /// </returns>
        /// <param name="key">A unique identifier for the cache entry to get. </param><param name="regionName">Optional. A named region in the cache to which the cache entry was added, if regions are implemented. The default value for the optional parameter is null.</param>
        object Get(string key, string regionName = null);

        /// <summary>
        /// When overridden in a derived class, gets the specified cache entry from the cache as a <see cref="T:System.Runtime.Caching.CacheItem"/> instance.
        /// </summary>
        /// 
        /// <returns>
        /// The cache entry that is identified by <paramref name="key"/>.
        /// </returns>
        /// <param name="key">A unique identifier for the cache entry to get. </param><param name="regionName">Optional. A named region in the cache to which the cache was added, if regions are implemented. Because regions are not implemented in .NET Framework 4, the default is null.</param>
        CacheItem GetCacheItem(string key, string regionName = null);

        /// <summary>
        /// When overridden in a derived class, inserts a cache entry into the cache, specifying time-based expiration details.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry. </param><param name="value">The object to insert.</param><param name="absoluteExpiration">The fixed date and time at which the cache entry will expire.</param><param name="regionName">Optional. A named region in the cache to which the cache entry can be added, if regions are implemented. The default value for the optional parameter is null.</param>
        void Set(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null);

        /// <summary>
        /// When overridden in a derived class, inserts the cache entry into the cache as a <see cref="T:System.Runtime.Caching.CacheItem"/> instance, specifying information about how the entry will be evicted.
        /// </summary>
        /// <param name="item">The cache item to add.</param><param name="policy">An object that contains eviction details for the cache entry. This object provides more options for eviction than a simple absolute expiration.</param>
        void Set(CacheItem item, CacheItemPolicy policy);

        /// <summary>
        /// When overridden in a derived class, inserts a cache entry into the cache.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry. </param><param name="value">The object to insert.</param><param name="policy">An object that contains eviction details for the cache entry. This object provides more options for eviction than a simple absolute expiration.</param><param name="regionName">Optional. A named region in the cache to which the cache entry can be added, if regions are implemented. The default value for the optional parameter is null.</param>
        void Set(string key, object value, CacheItemPolicy policy, string regionName = null);

        /// <summary>
        /// When overridden in a derived class, gets a set of cache entries that correspond to the specified keys.
        /// </summary>
        /// 
        /// <returns>
        /// A dictionary of key/value pairs that represent cache entries.
        /// </returns>
        /// <param name="keys">A collection of unique identifiers for the cache entries to get. </param><param name="regionName">Optional. A named region in the cache to which the cache entry or entries were added, if regions are implemented. The default value for the optional parameter is null.</param>
        IDictionary<string, object> GetValues(IEnumerable<string> keys, string regionName = null);

        /// <summary>
        /// Gets a set of cache entries that correspond to the specified keys.
        /// </summary>
        /// 
        /// <returns>
        /// A dictionary of key/value pairs that represent cache entries.
        /// </returns>
        /// <param name="regionName">Optional. A named region in the cache to which the cache entry or entries were added, if regions are implemented. Because regions are not implemented in .NET Framework 4, the default is null.</param><param name="keys">A collection of unique identifiers for the cache entries to get. </param>
        IDictionary<string, object> GetValues(string regionName, params string[] keys);

        /// <summary>
        /// When overridden in a derived class, removes the cache entry from the cache.
        /// </summary>
        /// 
        /// <returns>
        /// An object that represents the value of the removed cache entry that was specified by the key, or null if the specified entry was not found.
        /// </returns>
        /// <param name="key">A unique identifier for the cache entry. </param><param name="regionName">Optional. A named region in the cache to which the cache entry was added, if regions are implemented. The default value for the optional parameter is null.</param>
        object Remove(string key, string regionName = null);

        /// <summary>
        /// When overridden in a derived class, gets the total number of cache entries in the cache.
        /// </summary>
        /// 
        /// <returns>
        /// The number of cache entries in the cache. If <paramref name="regionName"/> is not null, the count indicates the number of entries that are in the specified cache region.
        /// </returns>
        /// <param name="regionName">Optional. A named region in the cache for which the cache entry count should be computed, if regions are implemented. The default value for the optional parameter is null.</param>
        long GetCount(string regionName = null);
    }
}
