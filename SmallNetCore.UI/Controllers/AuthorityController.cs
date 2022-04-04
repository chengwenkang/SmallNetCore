using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallNetCore.Common.Utils;
using SmallNetCore.IServices.Authority;
using SmallNetCore.Models.Base;
using SmallNetCore.Models.DBModels.FirstTestDb;
using SmallNetCore.Models.ViewModels.Base;
using SmallNetCore.Models.ViewModels.Request.Authority;
using SmallNetCore.Models.ViewModels.Response.Authority;

namespace SmallNetCore.UI.Controllers
{
    /// <summary>
    /// 权限相关服务
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        ILoginService loginService;
        IRoleService roleService;
        IMapper mapper;
        public AuthorityController(ILoginService _loginService, IRoleService _roleService, IMapper _mapper)
        {
            this.loginService = _loginService;
            this.roleService = _roleService;
            this.mapper = _mapper;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<LoginResponse> Login(LoginRequest request)
        {
            return loginService.Login(request);
        }

        /// <summary>
        /// 验证AutoMapper
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<UserViewModel> CheckAutoMapper(LoginRequest request)
        {
            var model = new User
            {
                Id = 1,
                UserName = "ccesefe",
                Sex = 0
            };

            var viewModel = mapper.Map<UserViewModel>(model);

            return new BaseResponse<UserViewModel>
            {
                Result = viewModel
            };
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<TokenModel> GetUserInfo2(int i)
        {
            return loginService.GetUserInfo(i);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public BaseResponse<TokenModel> GetUserInfo(int i)
        {
            return loginService.GetUserInfo(i);
        }

        /// <summary>
        /// TestCache
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [HttpPost]
        public User TestCache(int i)
        {
            var model = new User
            {
                Id = 1,
                UserName = "ccesefe",
                Sex = 0
            };

            CacheHelper.SetCache("sds", model, 3);

            var re = CacheHelper.GetCache("sds");

            return (User)re;
        }

        /// <summary>
        /// AddRole
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddRole(Role role)
        {
            return roleService.Add(role);
        }

        /// <summary>
        /// GetRoleList
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<Role> GetRoleList()
        {
            return roleService.Query();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> AddUser()
        {
            return loginService.AddUser();
        }
    }
}
