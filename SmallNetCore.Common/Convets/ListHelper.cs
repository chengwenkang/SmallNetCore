using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Common.Convets
{
    /// <summary>
    /// 集合扩展类
    /// </summary>
    public static class ListHelper
    {
        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个 System.Collections.Generic.List`1 中的第一个匹配元素
        /// 如果没有匹配的则返回new 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">集合</param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static T MyFind<T>(this IEnumerable<T> list, Predicate<T> match)
        {
            if (list != null)
            {
                T result = list.ToList().Find(match);
                if (result != null)
                {
                    return result;
                }
            }

            //返回默认值
            if (typeof(T).Name == nameof(String))
            {
                return default(T);
            }
            return (T)Activator.CreateInstance(typeof(T));
        }

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">集合</param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static List<T> MyFindAll<T>(this IEnumerable<T> list, Predicate<T> match)
        {
            if (!ListIsNullOrEmpty(list))
            {
                var result = list.ToList().FindAll(match);
                if (!ListIsNullOrEmpty(result))
                {
                    return result;
                }
            }

            //返回默认值
            return new List<T>();
        }

        /// <summary>
        /// 获取集合数量，避免NULL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">集合</param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static int MyCount<T>(this IEnumerable<T> list)
        {
            return list?.Count() ?? 0;
        }

        /// <summary>
        /// 自定义平均数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">集合</param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static decimal MyAverage<T>(this IEnumerable<T> list, Predicate<T> match, Func<T, decimal> selector, decimal defaultValue = 0)
        {
            var result = MyFindAll(list, match);
            if (!ListIsNullOrEmpty(result))
            {
                return result.Average(selector);
            }

            //返回默认值
            return defaultValue;
        }

        /// <summary>
        /// 判断一个List是否为空或者未初始化
        /// </summary>
        /// <typeparam name="T">任意Model</typeparam>
        /// <param name="list">任意实现Ilist接口的类型</param>
        /// <returns>返回true表示空，否则不为空</returns>
        public static bool ListIsNullOrEmpty<T>(IEnumerable<T> list)
        {
            return list == null || list.Count().Equals(0);
        }
    }
}
