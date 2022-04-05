using SmallNetCore.Common.ApIInfo;
using SmallNetCore.Common.Serialize;
using SmallNetCore.Models.Configs;
using SmallNetCore.Models.ViewModels.Base.Remotes;
using SmallNetCore.Models.ViewModels.Request.Remotes;
using SmallNetCore.Models.ViewModels.Response.Remotes;

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
            var result = HttpHelper.GetAsync(Consts.GetPlayersUrl);

            return JsonHelper.ToObject<FastmockBase<PlayResponse>>(result.Result);
        }

        /// <summary>
        /// 用于测试
        /// </summary>
        /// <returns></returns>
        public static FastmockBase<bool> CheckPlay(CheckPlayRequest request)
        {
            var result = HttpHelper.PostAsync(Consts.CheckPlay, JsonHelper.ToJson(request));

            return JsonHelper.ToObject<FastmockBase<bool>>(result.Result);
        }
    }
}
