using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Models.ViewModels.Base
{
    public class BasePageRequest
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }

    public class BasePageRequest<T> : BasePageRequest
    {
        //业务请求参数
        public T Request { get; set; }
    }
}
