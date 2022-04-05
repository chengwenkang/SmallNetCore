using SmallNetCore.Models.Base.Helper;
using SmallNetCore.Models.Entitys;
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
        /// 数据库链接
        /// </summary>
        public static List<DBConnConfigEntity> DBConfigs = AppsettingHelper.App<DBConnConfigEntity>(new string[] { "MYSQL" });
       
        #endregion
    }
}
