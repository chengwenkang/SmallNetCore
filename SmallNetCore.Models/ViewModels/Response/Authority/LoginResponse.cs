using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Models.ViewModels.Response.Authority
{
    public class LoginResponse
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; } = 0;

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
