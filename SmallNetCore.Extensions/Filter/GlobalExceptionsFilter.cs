using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmallNetCore.Common.Serialize;
using SmallNetCore.Models.Enums;
using SmallNetCore.Models.ViewModels.Base;
using System.Net;
using System.Web.Http;

namespace SmallNetCore.Extensions.Filter
{
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(GlobalExceptionsFilter));

        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                var json = new BaseResponse<string>
                {
                    ErrorMsg = context.Exception.Message,//错误信息
                    StatusCode = context.Exception is CustomException ? StatusCodeEnum.Fail : StatusCodeEnum.Exception  //如果是自定义的异常，则返回正常错误，否则按系统异常处理
                };

                if (context.Exception is HttpResponseException && ((HttpResponseException)context.Exception).Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    context.Result = new UnauthorizedResult();
                }
                else
                {
                    context.Result = new ContentResult
                    {
                        Content = JsonHelper.ToJson(json)
                    };
                }

                //TODO 根据自己的需要做日志记录
                log.Error(json.ErrorMsg, context.Exception);
            }
            context.ExceptionHandled = true;
        }
    }
}
