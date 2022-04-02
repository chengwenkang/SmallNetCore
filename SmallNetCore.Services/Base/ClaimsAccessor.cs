﻿using Microsoft.AspNetCore.Http;
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

        public ClaimsPrincipal UserPrincipal
        {
            get
            {
                ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
                if (user.Identity.IsAuthenticated)
                {
                    return user;
                }
                else
                {
                    var response = new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                    };

                    throw new HttpResponseException(response);
                }
            }
        }
    }
}