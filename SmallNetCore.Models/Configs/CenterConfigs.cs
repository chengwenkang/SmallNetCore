using SmallNetCore.Models.Base.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Models.Configs
{
    /// <summary>
    /// 配置中心
    /// </summary>
    public class CenterConfigs
    {
        #region AppSetting

        public static string Issuer = AppsettingHelper.App(new string[] { "JwtSetting", "Issuer" });
        public static string Audience = AppsettingHelper.App(new string[] { "JwtSetting", "Audience" });
        public static string SecretKey = AppsettingHelper.App(new string[] { "JwtSetting", "SecretKey" });
        public static string JWTExpireSeconds = AppsettingHelper.App(new string[] { "JwtSetting", "ExpireSeconds" });

        /// <summary>
        /// FirstDB数据库链接
        /// </summary>
        public static string FirstDB = AppsettingHelper.App(new string[] { "MYSQL", "ConnectionString" });

        /// <summary>
        /// SecondDB数据库链接
        /// </summary>
        public static string SecondDB = AppsettingHelper.App(new string[] { "MYSQL2", "ConnectionString" });
      
        #endregion
    }
}
