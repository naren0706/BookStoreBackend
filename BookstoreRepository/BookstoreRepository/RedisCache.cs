using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookstoreRepository.BookstoreRepository
{
    public class RedisCache
    {
        private readonly IDistributedCache distributedCache;
        public RedisCache(IDistributedCache distributedCache)
        {
            this.distributedCache=distributedCache;
        }
        public void PutListToCache(int userid,string key,object value)
        {
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
            var enlist = value;
            var jsonstring = JsonConvert.SerializeObject(enlist);
            distributedCache.SetString(key, jsonstring, options);
        }
        public List<object> GetListFromCache(string key)
        {
            var CacheString = this.distributedCache.GetString(key);
            return JsonConvert.DeserializeObject<IEnumerable<object>>(CacheString).ToList();
        }
    }
}
