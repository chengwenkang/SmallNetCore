using Microsoft.AspNetCore.Http;
using SmallNetCore.Common.Convets;
using SmallNetCore.IServices.Base;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace SmallNetCore.Services.Base
{
    public class ClaimsAccessor : IClaimsAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal UserPrincipal
        {
            get
            {
                ClaimsPrincipal? user = _httpContextAccessor?.HttpContext?.User;
                if (user?.Identity?.IsAuthenticated ?? false)
                {
                    return user;
                }

                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }

        /// <summary>
        /// 获取当前的登陆用户ID
        /// </summary>
        public int CurrentUserId
        {
            get
            {
                return UserPrincipal?.Identity?.Name?.ToInt() ?? 0;
            }
        }
    }
}
