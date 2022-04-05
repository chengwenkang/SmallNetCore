using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmallNetCore.Common.Serialize;
using SmallNetCore.Models.Enums;
using SmallNetCore.Models.ViewModels.Base;

namespace SmallNetCore.Extensions.Filter
{
    public class MyActionFilterAttribute : ActionFilterAttribute
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MyActionFilterAttribute));

        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var request = string.Empty;
            if (filterContext.ActionArguments.Any())
            {
                request = JsonHelper.ToJson(filterContext.ActionArguments);
            }

            //TODO 根据自己的需要做日志记录
            var logStr = $"请求参数：{request},TraceId:{Thread.GetCurrentProcessorId()}"; // 记录请求日志
            log.Info(logStr);
        }

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var response = JsonHelper.ToJson(filterContext.Result);
           
            //TODO 根据自己的需要做日志记录
            var logStr = $"返回参数：{response},TraceId:{Thread.GetCurrentProcessorId()}"; // 记录请求日志
            log.Info(logStr);
        }

        /// <summary>
        /// 在执行操作结果之前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);

            //参数验证
            if (!filterContext.ModelState.IsValid)
            {
                var json = new BaseResponse<string>
                {
                    ErrorMsg = filterContext.ModelState.Values.First()?.Errors[0].ErrorMessage?? String.Empty,//错误信息
                    StatusCode = StatusCodeEnum.Fail
                };

                filterContext.Result = new ContentResult
                {
                    Content = JsonHelper.ToJson(json)
                };

                return;
            }
         
            //TODO 根据自己的需要做日志记录
            var logStr = $"OnResultExecuting,TraceId:{Thread.GetCurrentProcessorId()}"; // 记录请求日志
            log.Info(logStr);
        }

        /// <summary>
        /// 在执行操作结果后
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

            //TODO 根据自己的需要做日志记录
            var logStr = $"OnResultExecuted,TraceId:{Thread.GetCurrentProcessorId()}"; // 记录请求日志
            log.Info(logStr);
        }
    }
}
