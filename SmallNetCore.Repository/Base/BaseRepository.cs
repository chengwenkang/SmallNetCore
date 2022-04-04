using SqlSugar;
using SqlSugar.IOC;
using System.Linq.Expressions;
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

        #region 查询实体,select()用法
        public async Task<TResult> GeT<TResult>(Expression<Func<T, bool>> predicate)
        {
            return await Context.Queryable<T>().Where(predicate).Select<TResult>().FirstAsync();
        }
        public async Task<TResult> GeT<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> predicate)
        {
            return await Context.Queryable<T>().Where(predicate).Select(expression).FirstAsync();
        }
        #endregion

        #region 查询列表，T查询扩展
        public async Task<List<T>> QueryList(string strOrderByFileds)
        {
            return await Context.Queryable<T>().OrderBy(strOrderByFileds).ToListAsync();
        }
        public async Task<List<T>> QueryList(Expression<Func<T, bool>> predicate, string strOrderByFileds)
        {
            return await Context.Queryable<T>().WhereIF(predicate != null, predicate).OrderByIF(strOrderByFileds != null, strOrderByFileds).ToListAsync();
        }
        public async Task<List<T>> QueryList(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderByExpression, bool isAsc = true)
        {
            return await Context.Queryable<T>().WhereIF(predicate != null, predicate).OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).ToListAsync();
        }
        #endregion

        #region 查询列表,select<dto>()用法
        /// <summary>
        /// 查询列表
        /// select<dto>()用法
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryList<TResult>(Expression<Func<T, bool>> predicate = null)
        {
            return await Context.Queryable<T>().WhereIF(predicate != null, predicate).Select<TResult>().ToListAsync();
        }
        public async Task<List<TResult>> QueryList<TResult>(string strOrderByFileds)
        {
            return await Context.Queryable<T>().OrderBy(strOrderByFileds).Select<TResult>().ToListAsync();
        }
        public async Task<List<TResult>> QueryList<TResult>(Expression<Func<T, object>> orderByExpression, bool isAsc = true)
        {
            return await Context.Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).Select<TResult>().ToListAsync();
        }

        public async Task<List<TResult>> QueryList<TResult>(Expression<Func<T, bool>> predicate, string strOrderByFileds)
        {
            return await Context.Queryable<T>().WhereIF(predicate != null, predicate).OrderByIF(strOrderByFileds != null, strOrderByFileds).Select<TResult>().ToListAsync();
        }
        public async Task<List<TResult>> QueryList<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderByExpression, bool isAsc = true)
        {
            return await Context.Queryable<T>().WhereIF(predicate != null, predicate).OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).Select<TResult>().ToListAsync();
        }
        #endregion

        #region 查询列表,select(expression)表达式用法
        /// <summary>
        /// 查询列表
        /// select(expression)表达式用法
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<TResult>> QueryList<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> predicate = null)
        {
            return await Context.Queryable<T>().WhereIF(predicate != null, predicate).Select(expression).ToListAsync();
        }
        public async Task<List<TResult>> QueryList<TResult>(Expression<Func<T, TResult>> expression, string strOrderByFileds)
        {
            return await Context.Queryable<T>().OrderBy(strOrderByFileds).Select(expression).ToListAsync();
        }
        public async Task<List<TResult>> QueryList<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, object>> orderByExpression, bool isAsc = true)
        {
            return await Context.Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).Select(expression).ToListAsync();
        }
        public async Task<List<TResult>> QueryList<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> predicate, string strOrderByFileds)
        {
            return await Context.Queryable<T>().WhereIF(predicate != null, predicate).OrderByIF(strOrderByFileds != null, strOrderByFileds).Select(expression).ToListAsync();
        }
        public async Task<List<TResult>> QueryList<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderByExpression, bool isAsc = true)
        {
            return await Context.Queryable<T>().WhereIF(predicate != null, predicate).OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).Select(expression).ToListAsync();
        }
        #endregion

        #region 事务委托
        /// <summary>
        /// 多租户异常事务
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task<DbResult<bool>> UseITenantTran(Func<Task> action)
        {
            var resultTran = await itenant.UseTranAsync(async () =>
            {
                await action();
            });

            return resultTran;
        }
        /// <summary>
        /// 同一对句事务处理
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task<DbResult<bool>> UseTran(Func<Task> action)
        {
            var resultTran = await Context.Ado.UseTranAsync(async () =>
            {
                await action();
            });
            return resultTran;
        }
        #endregion
    }
}
