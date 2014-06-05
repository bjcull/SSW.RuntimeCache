using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SSW.RuntimeCache.Tests
{
    [TestClass]
    public class CacheTests
    {
        [TestMethod]
        public void GivenSimpleValues_CorrectValueReturned()
        {
            var cache = new FunctionCacheProvider(new MemoryCache("Test"));

            var result = cache.ExecuteWithCache("Test1", () => SimpleAddition(2, 3), DateTimeOffset.Now.AddSeconds(10));

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void GivenSimpleValues_WithSlowMethod_EnsureCachedOnSecondPass()
        {
            var cache = new FunctionCacheProvider(new MemoryCache("Test"));
            var sw1 = new Stopwatch();
            
            sw1.Start();
            cache.ExecuteWithCache("Test2", () => SimpleAdditionSlow(2, 3, 2000), DateTimeOffset.Now.AddSeconds(10));
            sw1.Stop();

            var sw2 = new Stopwatch();

            sw2.Start();
            cache.ExecuteWithCache("Test2", () => SimpleAdditionSlow(2, 3, 2000), DateTimeOffset.Now.AddSeconds(10));
            sw2.Stop();

            Assert.IsTrue(sw1.ElapsedMilliseconds >= 2000 && sw2.ElapsedMilliseconds < 2000);
        }

        [TestMethod]
        public void GivenSimpleValues_WithSlowMethod_EnsureCacheExpires()
        {
            var cache = new FunctionCacheProvider(new MemoryCache("Test"));
            var sw1 = new Stopwatch();

            sw1.Start();
            cache.ExecuteWithCache("Test3", () => SimpleAdditionSlow(2, 3, 2000), DateTimeOffset.Now.AddSeconds(2));
            sw1.Stop();

            Thread.Sleep(5000);

            var sw2 = new Stopwatch();

            sw2.Start();
            cache.ExecuteWithCache("Test3", () => SimpleAdditionSlow(2, 3, 2000), DateTimeOffset.Now.AddSeconds(2));
            sw2.Stop();

            Assert.IsTrue(sw1.ElapsedMilliseconds >= 2000 && sw2.ElapsedMilliseconds >= 2000);
        }

        [TestMethod]
        public void GivenSimpleValues_CachedByParams_EnsureCachedOnSecondPass()
        {
            var cache = new FunctionCacheProvider(new MemoryCache("Test"));
            var sw1 = new Stopwatch();

            sw1.Start();
            cache.ExecuteWithCache("Test2", () => SimpleAdditionSlow(2, 3, 2000), DateTimeOffset.Now.AddSeconds(10), 2, 3);
            sw1.Stop();

            var sw2 = new Stopwatch();

            sw2.Start();
            cache.ExecuteWithCache("Test2", () => SimpleAdditionSlow(2, 3, 2000), DateTimeOffset.Now.AddSeconds(10), 2, 3);
            sw2.Stop();

            Assert.IsTrue(sw1.ElapsedMilliseconds >= 2000 && sw2.ElapsedMilliseconds < 2000);
        }

        [TestMethod]
        public void GivenSimpleValues_WithDifferentParams_EnsureCachedSeparately()
        {
            var cache = new FunctionCacheProvider(new MemoryCache("Test"));
            var sw1 = new Stopwatch();

            sw1.Start();
            cache.ExecuteWithCache("Test2", () => SimpleAdditionSlow(2, 3, 2000), DateTimeOffset.Now.AddSeconds(10), 2, 3);
            sw1.Stop();

            var sw2 = new Stopwatch();

            sw2.Start();
            cache.ExecuteWithCache("Test2", () => SimpleAdditionSlow(2, 3, 2000), DateTimeOffset.Now.AddSeconds(10), 2, 4);
            sw2.Stop();

            Assert.IsTrue(sw1.ElapsedMilliseconds >= 2000 && sw2.ElapsedMilliseconds >= 2000);
        }

        private int SimpleAddition(int value1, int value2)
        {
            return value1 + value2;
        }

        private int SimpleAdditionSlow(int value1, int value2, int sleep)
        {
            Thread.Sleep(sleep);
            return SimpleAddition(value1, value2);
        }
    }
}
