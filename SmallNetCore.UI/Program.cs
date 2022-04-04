using Autofac;
using Autofac.Extensions.DependencyInjection;
using SmallNetCore.Extensions;
using SmallNetCore.Extensions.Filter;
using SmallNetCore.Extensions.ServiceExtensions;
using SmallNetCore.Models.Base.Helper;

var builder = WebApplication.CreateBuilder(args);

#region 添加服务

builder.Services.AddControllers(o =>
{
    // 全局异常过滤
    o.Filters.Add(typeof(GlobalExceptionsFilter));

    // 方法执行前后
    o.Filters.Add(typeof(MyActionFilterAttribute));
});

//swagger文档
if (EnvironmentExt.IsTestEnv())
{
    builder.Services.AddSwaggerSetup();
}

//读取配置文件
builder.Services.AddSingleton(new AppsettingHelper(builder.Configuration));

//加入AutoMapper
builder.Services.AddAutoMapperSetup();

builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

//注册服务
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(item =>
{
    item.RegisterModule(new AutofacModuleRegister());//批量注册服务
});

//加入SqlSugar
builder.Services.AddSqlSugarSetup();

//加入JWT权限验证
builder.Services.AddAuthentication_JWTSetup();

//注入Log4Net
builder.Services.AddLogging(cfg =>
{
    cfg.AddLog4Net();//可以通过log4net.config 配置写入数据库/Mogodb/ES等
});

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

// 启用路由
app.UseRouting();

//鉴权授权
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
#endregion