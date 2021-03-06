using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Common.Serialize
{
    public class JsonHelper
    {
        /// <summary>
        /// JsonConvert.SerializeObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson<T>(T obj)
        {
            try
            {
                if (obj != null)
                {
                    return JsonConvert.SerializeObject(obj);
                }
            }
            catch (Exception ex)
            {
            }

            return string.Empty;
        }

        public static string ToJson(string obj)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(obj) && obj != "null")
                {
                    return JsonConvert.DeserializeObject<string>(obj);
                }
            }
            catch (Exception ex)
            {
            }

            return string.Empty;
        }
        /// <summary>
        /// JsonConvert.DeserializeObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ToObject<T>(string content)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
