using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.Extensions.Caching.Memory;

namespace WebPushNotif.Demo.Cache
{
    public class MemoryVapidTokenCache : IVapidTokenCache
    {

        private readonly IMemoryCache _memoryCache;

        public MemoryVapidTokenCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public string Get(string audience)
        {
            if (!_memoryCache.TryGetValue(audience, out string token))
            {
                token = null;
            }

            return token;
        }

        public void Put(string audience, DateTimeOffset expiration, string token)
        {
            _memoryCache.Set(audience, token, expiration);
        }

    }
}
