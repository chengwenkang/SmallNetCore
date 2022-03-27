using SmallNetCore.Common.Utils;
using SmallNetCore.Models.Enums;
using System.Runtime.Serialization;

namespace SmallNetCore.Extensions
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    public class CustomException : Exception, ISerializable
    {
        //记录异常的类型
        private CustomExceptionType exceptionType;

        /// <summary>
        /// 异常代码
        /// </summary>
        private int errorCode = 0;

        /// <summary>
        /// 异常信息
        /// </summary>
        private string errorMessage = string.Empty;

        private Exception exception;

        /// <summary>
        /// 异常代码
        /// </summary>
        public Exception Exception
        {
            get
            {
                return exception;
            }
            set { exception = value; }
        }

        /// <summary>
        /// 异常代码
        /// </summary>
        public int ErrorCode
        {
            get
            {
                return errorCode;
            }
            set { errorCode = value; }
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set { errorMessage = value; }
        }

        /// <summary>
        /// 返回自定义的错误信息
        /// </summary>
        public string CustomErrorMsg
        {
            get
            {
                return string.IsNullOrWhiteSpace(errorMessage) ? exceptionType.GetEnumDesc() : errorMessage;
            }
        }

        public CustomException(CustomExceptionType type) : base()
        {
            this.exceptionType = type;
            errorCode = (int)exceptionType;
            errorMessage = exceptionType.GetEnumDesc();
        }

        public CustomException(CustomExceptionType type, Exception ex) : base()
        {
            this.exceptionType = type;
            errorCode = (int)exceptionType;
            errorMessage = exceptionType.GetEnumDesc();
            exception = ex;
        }

        public CustomException(CustomExceptionType type, string errorMsg) : base()
        {
            this.exceptionType = type;
            errorCode = (int)exceptionType;
            errorMessage = $"{errorMsg}";
        }

        /// <summary>
        /// 返回自定义错误
        /// </summary>
        /// <param name="errorMsg"></param>
        public CustomException(string errorMsg) : base()
        {
            this.exceptionType = CustomExceptionType.DIYErrorMessage;
            errorCode = (int)exceptionType;
            errorMessage = $"{errorMsg}";
        }

        //序列化
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        //重写message方法,以让它显示相应异常提示信息
        public override string Message
        {
            get
            {
                return string.IsNullOrWhiteSpace(errorMessage) ? exceptionType.GetEnumDesc() : errorMessage;
            }
        }

        /// <summary>
        /// 重载ToString方法，拼接异常信息，如日志可以使用
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", errorCode, errorMessage, base.Message);
        }
    }
}
