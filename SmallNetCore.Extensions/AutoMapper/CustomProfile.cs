using AutoMapper;
using SmallNetCore.Models.DBModels.FirstTestDb;
using SmallNetCore.Models.ViewModels.Response.Authority;

namespace SmallNetCore.Extensions.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<Role, RoleViewModel>();
            CreateMap<User, UserViewModel>();
        }
    }
}
