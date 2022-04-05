using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Models.Entitys
{
    public class TokenModel
    {
        /// <summary>
        /// UId 目前只存放用户主键ID,用户的其他信息可以通过数据库或者缓存获取
        /// </summary>
        public int UId { get; set; }

        ///// <summary>
        ///// 姓名
        ///// </summary>
        //public string Name { get; set; }
    }
}
