using SmallNetCore.Models.Enums;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Models.DBModels.FirstTestDb
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Tenant(MySqlConnEnum.FisrtTestDb)]
    public class Role
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
    }
}
