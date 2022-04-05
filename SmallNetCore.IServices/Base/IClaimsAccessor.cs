using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.IServices.Base
{
    public interface IClaimsAccessor
    {
        //ClaimsPrincipal UserPrincipal { get; }

        int CurrentUserId { get; }
    }
}
