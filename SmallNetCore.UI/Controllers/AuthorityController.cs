using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmallNetCore.Models.ViewModels.Request;

namespace SmallNetCore.UI.Controllers
{
    /// <summary>
    /// 权限相关服务
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        /// <summary>
        /// 埋名小程序登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public string WXAppletLogin(LoginRequest request)
        {
            return "12";
        }
    }
}
