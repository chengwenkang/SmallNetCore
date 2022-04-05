using SmallNetCore.Models.Base;
using SmallNetCore.Models.ViewModels.Base;
using SmallNetCore.Models.ViewModels.Request.Authority;
using SmallNetCore.Models.ViewModels.Response.Authority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.IServices.Authority
{
    public interface ILoginService
    {
        public BaseResponse<LoginResponse> Login(LoginRequest request);

        public BaseResponse<TokenModel> GetUserInfo(int i);

        public BaseResponse<bool> AddUser(string name);
    }
}
