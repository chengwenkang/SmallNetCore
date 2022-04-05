using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Models.ViewModels.Base.Remotes
{
    public class FastmockBase<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string code { get; set; } = string.Empty;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string desc { get; set; } = string.Empty;

        /// <summary>
        /// 结果
        /// </summary>
        public T data { get; set; }
    }
}
