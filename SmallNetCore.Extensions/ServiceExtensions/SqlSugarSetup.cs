using Microsoft.Extensions.DependencyInjection;
using SmallNetCore.Models.Configs;
using SmallNetCore.Models.Enums;
using SqlSugar;
using SqlSugar.IOC;

namespace SmallNetCore.Extensions.ServiceExtensions
{
    public static class SqlSugarSetup
    {
        public static void AddSqlSugarSetup(this IServiceCollection services)
        {
            services.AddSqlSugar(new List<IocConfig>
            {
                 new IocConfig
                {
                  ConfigId=MySqlConnEnum.FisrtTestDb.ToString(),
                  DbType = IocDbType.MySql,
                  ConnectionString = CenterConfigs.FirstDB,
                  IsAutoCloseConnection = true,
                },

                new IocConfig
                {
                  ConfigId=MySqlConnEnum.SecondTestDb.ToString(),
                  DbType = IocDbType.MySql,
                  ConnectionString = CenterConfigs.SecondDB,
                  IsAutoCloseConnection = true,
                },
            });

            services.ConfigurationSugar(db =>
            {
                //里面可以循环
                db.GetConnection(MySqlConnEnum.FisrtTestDb).Aop.OnLogExecuting = (sql, p) =>
                {
                    Console.WriteLine(sql);
                };
                db.GetConnection(MySqlConnEnum.SecondTestDb).Aop.OnLogExecuting = (sql, p) =>
                {
                    Console.WriteLine(sql);
                };
            });
        }
    }
}
