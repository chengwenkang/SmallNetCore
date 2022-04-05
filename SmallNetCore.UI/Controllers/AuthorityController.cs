using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallNetCore.IServices.Authority;
using SmallNetCore.Models.DBModels.FirstTestDb;
using SmallNetCore.Models.Entitys;
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
    [Authorize]
    public class AuthorityController : ControllerBase
    {
        ILoginService loginService;
        IRoleService roleService;
      
        public AuthorityController(ILoginService _loginService, IRoleService _roleService, IMapper _mapper)
        {
            this.loginService = _loginService;
            this.roleService = _roleService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public BaseResponse<LoginResponse> Login(LoginRequest request)
        {
            return loginService.Login(request);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<TokenModel> GetUserInfo(int i)
        {
            return loginService.GetUserInfo(i);
        }

        /// <summary>
        /// AddRole
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddRole(string roleName)
        {
            return roleService.Add(roleName);
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
        /// 添加用户，完全是为了测试同库事务，逻辑忽略
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> AddUser(string name)
        {
            return loginService.AddUser(name);
        }
    }
}
