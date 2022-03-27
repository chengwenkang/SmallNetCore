using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Common.Convets
{
    public static class TimeHelper
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(DateTime time)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString();
        }
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        /// <summary>
        /// 转到固定格式 :yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToDateTimeFormat(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转到固定格式 :yyyy-MM-dd
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToDateFormat(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 检查时间转到固定格式 :yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToDateTimeFormatForCheck(this DateTime time)
        {
            return time <= new DateTime(2000, 1, 1) ? string.Empty : ToDateTimeFormat(time);
        }

        /// <summary>
        /// 获取当前月开始结束时间
        /// </summary>
        /// <returns></returns>
        public static (DateTime StartTime, DateTime EndTime) CurrentMonth()
        {
            var type = "Month";
            return (GetTimeStartByType(type), DateTime.Now);
        }

        /// <summary>
        /// 获取当前周开始结束时间
        /// </summary>
        /// <returns></returns>
        public static (DateTime StartTime, DateTime EndTime) CurrentWeek()
        {
            var type = "Week";
            return (GetTimeStartByType(type), DateTime.Now);
        }

        #region 获取 本周、本月、本季度、本年 的开始时间或结束时间
        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="TimeType">Week、Month、Season、Year</param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime GetTimeStartByType(string TimeType)
        {
            DateTime now = DateTime.Today;
            switch (TimeType)
            {
                case "Week":
                    return now.AddDays(-(int)now.DayOfWeek + 1);
                case "Month":
                    return now.AddDays(-now.Day + 1);
                case "Season":
                    var time = now.AddMonths(0 - ((now.Month - 1) % 3));
                    return time.AddDays(-time.Day + 1);
                case "Year":
                    return now.AddDays(-now.DayOfYear + 1);
                default:
                    return DateTime.Now;
            }
        }

        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="TimeType">Week、Month、Season、Year</param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime GetTimeEndByType(string TimeType, DateTime now)
        {
            switch (TimeType)
            {
                case "Week":
                    return now.AddDays(7 - (int)now.DayOfWeek);
                case "Month":
                    return now.AddMonths(1).AddDays(-now.AddMonths(1).Day + 1).AddDays(-1);
                case "Season":
                    var time = now.AddMonths((3 - ((now.Month - 1) % 3) - 1));
                    return time.AddMonths(1).AddDays(-time.AddMonths(1).Day + 1).AddDays(-1);
                case "Year":
                    var time2 = now.AddYears(1);
                    return time2.AddDays(-time2.DayOfYear);
                default:
                    return DateTime.Now;
            }
        }
        #endregion

        /// <summary>
        /// 转换时间格式数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToDateString(string str)
        {
            if (str.Length != 8)
                return string.Empty;
            string newStr = string.Format("{0}-{1}-{2}", str.Substring(0, 4), str.Substring(4, 2), str.Substring(6, 2));
            return newStr;
        }

        /// <summary>
        /// 获取相差的天数
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static int GetSubtractDays(DateTime startTime, DateTime endTime)
        {
            TimeSpan span = endTime.Subtract(startTime);
            return span.Days;
        }
    }
}
