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
            var dbConfigs = CenterConfigs.DBConfigs.Select(t => new IocConfig
            {
                ConfigId = t.Database,
                DbType = IocDbType.MySql,
                ConnectionString = t.ConnectionString,
                IsAutoCloseConnection = true,
            });
            services.AddSqlSugar(dbConfigs.ToList());

            //services.ConfigurationSugar(db =>
            //{
            //    //里面可以循环
            //    db.GetConnection(MySqlConnEnum.FisrtTestDb).Aop.OnLogExecuting = (sql, p) =>
            //    {
            //        Console.WriteLine(sql);
            //    };
            //    db.GetConnection(MySqlConnEnum.SecondTestDb).Aop.OnLogExecuting = (sql, p) =>
            //    {
            //        Console.WriteLine(sql);
            //    };
            //});
        }
    }
}
