namespace SmallNetCore.Extensions
{
    /// <summary>
    /// 环境扩展
    /// </summary>
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
