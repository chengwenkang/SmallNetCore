using SmallNetCore.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Services.Base
{
    public class BaseService<T> : BaseRepository<T> where T : class, new()
    { 

    }
}
