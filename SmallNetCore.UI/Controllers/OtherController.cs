using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmallNetCore.Common.Utils;
using SmallNetCore.Models.DBModels.FirstTestDb;
using SmallNetCore.Models.ViewModels.Base;
using SmallNetCore.Models.ViewModels.Base.Remotes;
using SmallNetCore.Models.ViewModels.Request.Remotes;
using SmallNetCore.Models.ViewModels.Response.Authority;
using SmallNetCore.Models.ViewModels.Response.Remotes;
using SmallNetCore.Remotes.TestServices;

namespace SmallNetCore.UI.Controllers
{
    /// <summary>
    /// 其他相关测试
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OtherController : ControllerBase
    {
        IMapper mapper;
        public OtherController(IMapper _mapper)
        {
            this.mapper = _mapper;
        }

        #region 远程服务接口测试

        /// <summary>
        /// 用于远程服务Get测试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public FastmockBase<PlayResponse> GetPlays()
        {
            return FastmockService.GetPlays();
        }

        /// <summary>
        /// 用于远程服务Post测试
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public FastmockBase<bool> CheckPlay(CheckPlayRequest request)
        {
            return FastmockService.CheckPlay(request);
        }

        #endregion

        #region AutoMapper测试

        /// <summary>
        /// 验证AutoMapper
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<UserViewModel> CheckAutoMapper()
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

        #endregion

        #region 本地缓存测试

        /// <summary>
        /// 本地缓存测试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public User TestCache()
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

        #endregion
    }
}
