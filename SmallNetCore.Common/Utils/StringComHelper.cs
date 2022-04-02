using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Common.Utils
{
    public class StringComHelper
    {
        /// <summary>
        /// 截取指定字符串长度（不区分中英文，只按长度进行截取）  add by LiYundong at 2010-01-26 10:09:46
        /// </summary>
        /// <param name="stringToSub">待截取的字符串</param>
        /// <param name="length">需要截取的长度</param>
        /// <param name="endstring">超出长度显示的字符（此字段在参数isUser为true时有效）</param>
        /// <returns></returns>
        public static string GetSubString(string stringToSub, int length, string endstring = "...")
        {
            if (string.IsNullOrEmpty(stringToSub))
            {
                return string.Empty;
            }

            if (stringToSub.Length <= length)
                return stringToSub;

            return (stringToSub.Substring(0, length) + endstring);
        }

        /// <summary>
        /// 中文逗号转英文逗号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceComma(string str)
        {
            return str.Replace("，", ",");
        }
    }
}
