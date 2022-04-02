using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace SmallNetCore.Extensions.ServiceExtensions
{
    /// <summary>
    /// Swagger 启动服务
    /// </summary>
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "这是文档标题",
                    Version = "文档版本编号",
                    Description = "文档描述"
                });

                var strPath = Directory.GetCurrentDirectory();
                var file = Path.Combine(AppContext.BaseDirectory, "SmallNetCore.UI.xml");  // xml文档绝对路径
                var file2 = Path.Combine(AppContext.BaseDirectory, "SmallNetCore.Models.xml");  // xml文档绝对路径
                c.IncludeXmlComments(file, true); // true : 显示控制器层注释
                c.IncludeXmlComments(file2);

                // 开启加权小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // 在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // Jwt Bearer 认证，必须是 oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }
    }
}
