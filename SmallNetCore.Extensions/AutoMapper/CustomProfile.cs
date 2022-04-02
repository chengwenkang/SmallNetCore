using AutoMapper;
using SmallNetCore.Models.DBModels;
using SmallNetCore.Models.ViewModels.Response.Authority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Extensions.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<Article, ArticleViewModel>();
            CreateMap<User, UserViewModel>();
        }
    }
}
