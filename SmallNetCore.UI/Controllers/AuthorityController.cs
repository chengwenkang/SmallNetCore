using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallNetCore.IServices.Authority;
using SmallNetCore.Models.Base;
using SmallNetCore.Models.DBModels;
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
        IMapper mapper;
        public AuthorityController(ILoginService _loginService, IMapper _mapper)
        {
            this.loginService = _loginService;
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
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<ArticleViewModel> CheckAutoMapper(LoginRequest request)
        {
            var model = new Article
            {
                CreateTime = DateTime.Now,
                Id = 1,
                Remark = "ccesefe"
            };

            var viewModel = mapper.Map<ArticleViewModel>(model);

            return new BaseResponse<ArticleViewModel>
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
    }
}
