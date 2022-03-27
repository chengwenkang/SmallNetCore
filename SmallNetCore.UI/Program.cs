using Microsoft.OpenApi.Models;
using SmallNetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region 添加服务

builder.Services.AddControllers();

//swagger文档
if (EnvironmentExt.IsTestEnv())
{
    builder.Services.AddSwaggerGen(c =>
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
    });
}

var app = builder.Build();
#endregion

#region 启用中间件相关

//swagger文档
if (EnvironmentExt.IsTestEnv())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.Run();
#endregion


