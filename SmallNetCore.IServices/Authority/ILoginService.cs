using SmallNetCore.Models.Entitys;
using SmallNetCore.Models.ViewModels.Base;
using SmallNetCore.Models.ViewModels.Request.Authority;
using SmallNetCore.Models.ViewModels.Response.Authority;

namespace SmallNetCore.IServices.Authority
{
    public interface ILoginService
    {
        public BaseResponse<LoginResponse> Login(LoginRequest request);

        public BaseResponse<TokenModel> GetUserInfo(int i);

        public BaseResponse<bool> AddUser(string name);
    }
}
