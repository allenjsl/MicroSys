using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web;

namespace EyouSoft.Toolkit
{
    /// <summary>
    /// cache helper
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Add(string key, object value)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Add(key, value, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12), CacheItemPriority.Default, null);
        }

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        public static void Add(string key, object value, DateTime absoluteExpiration)
        {
            Cache cache = HttpRuntime.Cache;

            cache.Add(key, value, null, absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            Cache cache = HttpRuntime.Cache;
            var obj=cache.Get(key);
            return obj;
        }

        /// <summary>
        /// 清除缓存对象
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Remove(key);
        }
    }
}
