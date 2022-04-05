using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.IRepository.Base
{
    public interface IBaseRepository<T> : ISugarRepository, ISimpleClient<T> where T : class, new()
    {
        DbResult<bool> UseTran(Action<ISqlSugarClient> action);

        DbResult<bool> UseMutliTran(Action action);
    }
}
