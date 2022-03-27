using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Common.Utils
{
    /// <summary>
    /// 枚举的扩展功能类
    /// </summary>
    public static class EnumHelper
    {
        #region 获取枚举的描述信息

        /// <summary>
        /// 获取枚举的描述信息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetEnumDesc(this System.Enum e)
        {
            try
            {
                FieldInfo enumInfo = e.GetType().GetField(e.ToString());
                var enumAttributes
                    = (DescriptionAttribute[])enumInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return enumAttributes.Length > 0 ? enumAttributes[0].Description : e.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取枚举值的文本信息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetEnumValueStr(this Enum e)
        {
            try
            {
                return e.GetHashCode().ToString();
            }
            catch
            {
                return string.Empty;
            }

        }

        #endregion


        /// <summary>
        /// 根据值获取枚举值
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="name">枚举值名称</param>
        /// <returns></returns>
        public static System.Enum GetEnumByName(this Type enumType, string name)
        {
            foreach (object enumValue in System.Enum.GetValues(enumType))
            {
                var e = (System.Enum)enumValue;
                if (e.ToString() == name)
                    return e;
            }
            return null;
        }

        /// <summary>
        /// 获取枚举的描述信息，根据传入的枚举值
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDesc(Type enumType, byte enumValue)
        {
            string result = "";
            foreach (var e in System.Enum.GetValues(enumType).Cast<System.Enum>().Where(e => Convert.ToInt32(e) == enumValue))
            {
                result = GetEnumDesc(e);
                break;
            }

            return result;
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDesc(Type e, int? value)
        {
            FieldInfo[] fields = e.GetFields();
            for (int i = 1, count = fields.Length; i < count; i++)
            {
                if ((int)System.Enum.Parse(e, fields[i].Name) == value)
                {
                    DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])fields[i].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (EnumAttributes.Length > 0)
                    {
                        return EnumAttributes[0].Description;
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// 获取枚举的值与描述(定制化)
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static Dictionary<long, string> GetEnumValeLongDic(Type enumType)
        {
            var SMPramKey = new Dictionary<long, string>();
            foreach (object enumValue in System.Enum.GetValues(enumType))
            {
                var e = (System.Enum)enumValue;
                SMPramKey.Add(enumValue.GetHashCode(), GetEnumDesc(e));
            }
            return SMPramKey;
        }
       
        /// <summary>
        /// 根据描述获取枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetEnumByDescription<T>(string description)
        {
            try
            {
                Type _type = typeof(T);
                foreach (FieldInfo field in _type.GetFields())
                {
                    object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
                    if (objs.Length > 0 && (objs[0] as DescriptionAttribute).Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }

                return default(T);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
