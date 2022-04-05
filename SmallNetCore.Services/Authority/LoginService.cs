using SmallNetCore.Extensions;
using SmallNetCore.IRepository.FirstTestDb;
using SmallNetCore.IRepository.SecondTestDb;
using SmallNetCore.IServices.Authority;
using SmallNetCore.IServices.Base;
using SmallNetCore.Models.Base;
using SmallNetCore.Models.DBModels.FirstTestDb;
using SmallNetCore.Models.ViewModels.Base;
using SmallNetCore.Models.ViewModels.Request.Authority;
using SmallNetCore.Models.ViewModels.Response.Authority;
using static SmallNetCore.Models.ViewModels.Base.CommonResponse;

namespace SmallNetCore.Services.Authority
{
    public class LoginService : ILoginService
    {
        IClaimsAccessor claimsAccessor;
        IUserRepository userRepository;
        IRoleRepository roleRepository;
        IOrderRepository orderRepository;

        public LoginService(IClaimsAccessor _claimsAccessor, IUserRepository _userRepository, IRoleRepository _roleRepository, IOrderRepository _orderRepository)
        {
            this.claimsAccessor = _claimsAccessor;
            this.userRepository = _userRepository;
            this.roleRepository = _roleRepository;
            this.orderRepository = _orderRepository;
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
