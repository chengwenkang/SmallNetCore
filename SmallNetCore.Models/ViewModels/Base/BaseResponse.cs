using SmallNetCore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Models.ViewModels.Base
{
    /// <summary>
    /// 基础返回类
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public StatusCodeEnum StatusCode { get; set; } = StatusCodeEnum.Success;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; } = string.Empty;

    }

    /// <summary>
    /// 带结果的基础返回类
    /// </summary>
    public class BaseResponse<T> : BaseResponse
    {
        /// <summary>
        /// 结果
        /// </summary>
        public T Result { get; set; }
    }

    /// <summary>
    /// 分页的基础返回类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePageResponse<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页数量
        /// </summary>
        public int PageSize { get; set; } = 5;

        /// <summary>
        /// 总数
        /// </summary>
        public int Totals { get; set; } = 0;

        /// <summary>
        /// 返回值
        /// </summary>
        public List<T> Records { get; set; } = new List<T>();
    }

    /// <summary>
    /// 返回的格式封装
    /// </summary>
    public class CommonResponse
    {
        /// <summary>
        /// 返回正确信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static BaseResponse<T> GetOK<T>(T result, string msg = "")
        {
            return new BaseResponse<T>
            {
                Result = result,
                ErrorMsg = msg,
                StatusCode = StatusCodeEnum.Success
            };
        }

        /// <summary>
        /// 返回正确信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static BaseResponse<T> GetError<T>(T result, string msg = "")
        {
            return new BaseResponse<T>
            {
                Result = result,
                ErrorMsg = msg,
                StatusCode = StatusCodeEnum.Fail
            };
        }

        /// <summary>
        /// 返回正确信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static BaseResponse GetOK(string msg = "")
        {
            return new BaseResponse
            {
                ErrorMsg = msg,
                StatusCode = StatusCodeEnum.Success
            };
        }

        /// <summary>
        /// 返回正确信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static BaseResponse GetError(string msg = "")
        {
            return new BaseResponse
            {
                ErrorMsg = msg,
                StatusCode = StatusCodeEnum.Fail
            };
        }
    }
}
