using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Extensions
{
    public class EnvironmentExt
    {
        /// <summary>
        /// 是否是线下环境
        /// </summary>
        /// <returns></returns>
        public static bool IsTestEnv()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        }
    }
}
