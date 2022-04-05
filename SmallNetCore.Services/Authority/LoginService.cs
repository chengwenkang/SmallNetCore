using SmallNetCore.Common.Convets;
using SmallNetCore.Extensions;
using SmallNetCore.IRepository.FirstTestDb;
using SmallNetCore.IRepository.SecondTestDb;
using SmallNetCore.IServices.Authority;
using SmallNetCore.IServices.Base;
using SmallNetCore.Models.Base;
using SmallNetCore.Models.DBModels.FirstTestDb;
using SmallNetCore.Models.Entitys;
using SmallNetCore.Models.ViewModels.Base;
using SmallNetCore.Models.ViewModels.Request.Authority;
using SmallNetCore.Models.ViewModels.Response.Authority;
using static SmallNetCore.Models.ViewModels.Base.CommonResponse;

namespace SmallNetCore.Services.Authority
{
    public class LoginService : ILoginService
    {
        IClaimsAccessor claimsAccessor;
        IRoleRepository roleRepository;

        public LoginService(IClaimsAccessor _claimsAccessor, IRoleRepository _roleRepository)
        {
            this.claimsAccessor = _claimsAccessor;
            this.roleRepository = _roleRepository;
        }

        public BaseResponse<TokenModel> GetUserInfo(int i)
        {
            return GetOK(new TokenModel
            {
                UId = claimsAccessor.CurrentUserId
            });
        }

        public BaseResponse<LoginResponse> Login(LoginRequest request)
        {
            var token = JwtHelper.GetToken(new TokenModel
            {
                UId = 123
            });

            return GetOK(new LoginResponse
            {
                Token = token,
                UserId = 123
            });
        }

        public BaseResponse<bool> AddUser(string name)
        {
            Role roleEntity = new()
            {
                RoleName = "ces"
            };

            User userEntity = new()
            {
                Sex = 0,
                UserName = name
            };

            //Í¬¿âÊÂÎñ
            var resultTran = roleRepository.UseTran((context) =>
            {
                var t1 = context.Insertable(roleEntity).ExecuteCommand();
                var t2 = context.Insertable(userEntity).ExecuteCommand();
            });

            return GetOK(true);
        }
    }
}
