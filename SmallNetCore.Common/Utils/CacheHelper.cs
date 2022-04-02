using Microsoft.Extensions.Caching.Memory;


namespace SmallNetCore.Common.Utils
{
    public class CacheHelper
    {
        static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

        /// <summary>  
        /// 获取当前应用程序指定key的Cache值
        /// </summary>  
        /// <param name="key">  
        /// <returns></returns>y  
        public static object GetCache(string key)
        {
            if (!string.IsNullOrEmpty(key) && Cache.TryGetValue(key, out var val))
            {
                return val;
            }

            return default(object);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds">过期时间 单位秒</param>
        public static void SetCache(string key, object value, int seconds)
        {
            try
            {
                if (value == null) return;

                Cache.Set(key, value, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(seconds)
                });

            }
            catch (Exception)
            {
                //throw;    
            }
        }

        /// <summary>
        /// 清除单一键缓存  
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveKeyCache(string key)
        {
            try
            {
                Cache.Remove(key);
            }
            catch { }
        }
    }
}
