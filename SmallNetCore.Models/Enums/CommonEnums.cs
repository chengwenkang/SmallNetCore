using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Models.Enums
{
    public enum StatusCodeEnum
    {
        Success = 0,
        Fail = 1,
        Exception = 2
    }

    public enum LogTypeEnum
    {
        Info = 0,
        Warn = 1,
        Error = 2,
    }

    /// <summary>
    /// Êý¾Ý¿âÁ´½Ó
    /// </summary>
    public enum MySqlConnEnum
    {
        FisrtTestDb,
        SecondTestDb,
    }
}
