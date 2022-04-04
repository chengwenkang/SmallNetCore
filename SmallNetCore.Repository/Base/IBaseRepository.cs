using SqlSugar;
using System.Linq.Expressions;

namespace SmallNetCore.Repository.Base
{
    public interface IBaseRepository<T> : ISimpleClient<T> where T : class, new()
    {
        #region 查询实体,select()用法
        Task<TResult> GeT<TResult>(Expression<Func<T, bool>> predicate);
        Task<TResult> GeT<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> predicate);
        #endregion

        #region 查询列表，T查询扩展
        Task<List<T>> QueryList(string strOrderByFileds);
        Task<List<T>> QueryList(Expression<Func<T, bool>> predicate, string strOrderByFileds);
        Task<List<T>> QueryList(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderByExpression, bool isAsc = true);
        #endregion

        #region 查询列表,select<dto>()用法
        Task<List<TResult>> QueryList<TResult>(Expression<Func<T, bool>> predicate = null);
        Task<List<TResult>> QueryList<TResult>(string strOrderByFileds);
        Task<List<TResult>> QueryList<TResult>(Expression<Func<T, object>> orderByExpression, bool isAsc = true);
        Task<List<TResult>> QueryList<TResult>(Expression<Func<T, bool>> predicate, string strOrderByFileds);
        Task<List<TResult>> QueryList<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderByExpression, bool isAsc = true);
        #endregion

        #region 查询列表,select(expression)表达式用法
        Task<List<TResult>> QueryList<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> predicate = null);
        Task<List<TResult>> QueryList<TResult>(Expression<Func<T, TResult>> expression, string strOrderByFileds);
        Task<List<TResult>> QueryList<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, object>> orderByExpression, bool isAsc = true);
        Task<List<TResult>> QueryList<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> predicate, string strOrderByFileds);
        Task<List<TResult>> QueryList<TResult>(Expression<Func<T, TResult>> expression, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderByExpression, bool isAsc = true);
        #endregion

        #region 事物委托
        Task<DbResult<bool>> UseITenantTran(Func<Task> action);
        Task<DbResult<bool>> UseTran(Func<Task> action);
        #endregion
    }
}
