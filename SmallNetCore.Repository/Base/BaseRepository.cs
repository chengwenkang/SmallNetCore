using SmallNetCore.IRepository.Base;
using SqlSugar;
using SqlSugar.IOC;
using System.Reflection;

namespace SmallNetCore.Repository.Base
{
    public class BaseRepository<T> : SimpleClient<T>, IBaseRepository<T> where T : class, new()
    {
        protected ITenant itenant = null;//多租户事务

        /// <summary>
        /// 构造函数传入的context 没用 继承的基类只需要传入null就行了
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(ISqlSugarClient context = null) : base(context)
        {
            //通过特性拿到ConfigId
            var configId = typeof(T).GetCustomAttribute<TenantAttribute>()?.configId;
            if (configId == null)
            {
                throw new Exception($"{nameof(T)}实体没用配置库的属性TenantAttribute");
            }
            base.Context = DbScoped.SugarScope.GetConnection(configId);//子Db无租户方法，其他功能都有
            itenant = DbScoped.SugarScope;//设置租户接口
        }

        #region 扩展方法

        /// <summary>
        /// 同库处理事务
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public DbResult<bool> UseTran(Action<ISqlSugarClient> action)
        {
            var resultTran = base.Context.Ado.UseTran(() =>
           {
               action(base.Context);
           });

            return resultTran;
        }

        /// <summary>
        /// 多租户事务
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public DbResult<bool> UseMutliTran(Action action)
        {
            var resultTran = itenant.UseTran(() =>
           {
               action();
           });

           return resultTran;
        }

        #endregion
    }
}
