using SmallNetCore.Common.ApIInfo;
using SmallNetCore.Common.Serialize;
using SmallNetCore.Models.ViewModels.Base.Remotes;
using SmallNetCore.Models.ViewModels.Response.Remotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Remotes.TestServices
{
    public class FastmockService
    {
        /// <summary>
        /// 用于测试
        /// </summary>
        /// <returns></returns>
        public static FastmockBase<PlayResponse> GetPlays()
        {
            var result = HttpHelper.GetAsync("https://www.fastmock.site/mock/37e23916a9f1690d9acec5d71ba1f3cb/Test/GetPlayers");

            return JsonHelper.ToObject<FastmockBase<PlayResponse>>(result.Result);
        }
    }
}
