using Microsoft.OpenApi.Models;
using SmallNetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region ��ӷ���

builder.Services.AddControllers();

//swagger�ĵ�
if (EnvironmentExt.IsTestEnv())
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "�����ĵ�����",
            Version = "�ĵ��汾���",
            Description = "�ĵ�����"
        });

        var strPath = Directory.GetCurrentDirectory();
        var file = Path.Combine(AppContext.BaseDirectory, "SmallNetCore.UI.xml");  // xml�ĵ�����·��
        var file2 = Path.Combine(AppContext.BaseDirectory, "SmallNetCore.Models.xml");  // xml�ĵ�����·��
        c.IncludeXmlComments(file, true); // true : ��ʾ��������ע��
        c.IncludeXmlComments(file2);
    });
}

var app = builder.Build();
#endregion

#region �����м�����

//swagger�ĵ�
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


