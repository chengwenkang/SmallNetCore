using SmallNetCore.Extensions;
using SmallNetCore.IServices.Authority;
using SmallNetCore.IServices.Base;
using SmallNetCore.Models.Base;
using SmallNetCore.Models.DBModels.FirstTestDb;
using SmallNetCore.Models.ViewModels.Base;
using SmallNetCore.Models.ViewModels.Request.Authority;
using SmallNetCore.Models.ViewModels.Response.Authority;
using SmallNetCore.Services.Base;
using static SmallNetCore.Models.ViewModels.Base.CommonResponse;

namespace SmallNetCore.Services.Authority
{
    public class LoginService : BaseService<User>, ILoginService
    {
        IClaimsAccessor claimsAccessor;

        public LoginService(IClaimsAccessor _claimsAccessor)
        {
            this.claimsAccessor = _claimsAccessor;
        }

        public BaseResponse<TokenModel> GetUserInfo(int i)
        {
            return GetOK(new TokenModel
            {
                Name = claimsAccessor.UserPrincipal.Identity.Name
            });
        }

        public BaseResponse<LoginResponse> Login(LoginRequest request)
        {
            var token = JwtHelper.GetToken(new TokenModel
            {
                Name = "test",
                UId = 123
            });

            return GetOK(new LoginResponse
            {
                Token = token,
                UserId = 123
            });
        }

        public BaseResponse<bool> AddUser()
        {
            Role roleEntity = new()
            {
                RoleName = "ces"
            };

            User userEntity = new()
            {
                Sex = 0,
                UserName = "moon"
            };

            var resultTran = Context.Ado.UseTran(() =>
            {
                var t1 = Context.Insertable(roleEntity).ExecuteCommand();
                var t2 = Context.Insertable(userEntity).ExecuteCommand();
            });

            return GetOK(resultTran.Data);
        }
    }
}
