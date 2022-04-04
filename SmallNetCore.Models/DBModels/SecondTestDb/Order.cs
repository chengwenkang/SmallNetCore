using SmallNetCore.Models.Enums;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Models.DBModels.SecondTestDb
{
    [Tenant(MySqlConnEnum.SecondTestDb)]
    public class Order
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int OrderId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductId { get; set; }
    }
}
