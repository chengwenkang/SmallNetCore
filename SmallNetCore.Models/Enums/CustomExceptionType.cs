using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Models.Enums
{
    /// <summary>
    /// 自定义错误
    /// </summary>
    public enum CustomExceptionType
    {
        [Description("系统异常")]
        UnknownError = -1,
        [Description("成功")]
        Success = 0,
        [Description("无效签名")]
        IncorrectSign = 1001,
        [Description("参数错误")]
        ParamError = 1002,
        [Description("请求参数为空")]
        RequestParamNull = 1003,
        [Description("自定义描述异常")]
        DIYErrorMessage = 1004,
        [Description("查找无数据")]
        NoExitData = 1005,
        [Description("请求已过期")]
        OverTime = 1115,
    
    }
}
