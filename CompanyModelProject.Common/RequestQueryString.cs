/* =============================================
 * 名    称：RequestQueryString
 * 功能描述：RequestQueryString
 * 作    者： joanne.qv
 * 添加时间：2015/4/1 22:17:23
 * =============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;



namespace CompanyModelProject.Common
{
    /// <summary>
    /// 正则表达式规则格式工具类
    /// </summary>
    public static class RegexHelper
    {
        public const string SafeSqlString = @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']";
    }

    /// <summary>
    /// 统一请求的参数处理
    /// </summary>
    public class RequestQueryString
    {
        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <returns></returns>
        public static string GetCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null &&
                !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[cookieName].Value))
            {
                return HttpContext.Current.Request.Cookies[cookieName].Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            return (GetQueryString(strName, false));
        }
        public static string GetQueryString(string strName, string defaults)
        {
            var result = (GetQueryString(strName, false));
            return string.IsNullOrEmpty(result) ? defaults : result;
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary> 
        /// <param name="strName">Url参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current == null)
            {
                return "";
            }
            string queryString = HttpContext.Current.Request.QueryString[strName];
            string encoding = HttpContext.Current.Request.QueryString["encoding"];
            try
            {
                if (queryString == null)
                {
                    return "";
                }
                if (!string.IsNullOrWhiteSpace(encoding))
                {
                    //如果编码格式没传或者值为空，则不进行解码
                    queryString = HttpUtility.UrlDecode(queryString, System.Text.Encoding.GetEncoding(encoding));
                }
                if (sqlSafeCheck && !IsSafeSqlString(queryString))
                {
                    return "unsafe string";
                }
                return queryString.Trim();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName)
        {
            return (GetFormString(strName, false));
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, RegexHelper.SafeSqlString);
        }
        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
                return "";

            if (sqlSafeCheck && !IsSafeSqlString(HttpContext.Current.Request.Form[strName]))
                return "unsafe string";

            return HttpContext.Current.Request.Form[strName].Trim();
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            return GetString(strName, false);
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName, bool sqlSafeCheck)
        {
            if ("".Equals(GetQueryString(strName)))
            {
                return GetFormString(strName, sqlSafeCheck);
            }
            return GetQueryString(strName, sqlSafeCheck);
        }
        /// <summary>
        /// int型转换为string型
        /// </summary>
        /// <returns>转换后的string类型结果</returns>
        public static string IntToStr(int intValue)
        {
            return Convert.ToString(intValue);
        }
        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object expression, int defValue)
        {
            return ObjectToInt(expression, defValue);
        }

        /// <summary>
        /// 将对象转换为Int类型
        /// </summary>
        /// <param name="expression">要转换的对象</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjectToInt(object expression)
        {
            return ObjectToInt(expression, 0);
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="expression">要转换的对象</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ObjectToInt(object expression, int defValue)
        {
            if (expression != null)
            {
                return StrToInt(expression.ToString(), defValue);
            }
            return defValue;
        }

        /// <summary>
        /// 将字符串转换为Int类型,转换失败返回0
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string str)
        {
            return StrToInt(str, 0);
        }


        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(string str, int defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 ||
                !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
            {
                return defValue;
            }
            int rv;
            Int32.TryParse(str, out rv);
            return rv;
        }

        /// <summary>
        /// 将对象转换为Long位类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static long StrToLong(string str, long defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 20 ||
                !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
            {
                return defValue;
            }
            long rv;
            long.TryParse(str, out rv);
            return rv;
        }


        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的float类型结果</returns>
        public static float StrToFloat(string strValue, float defValue)
        {
            float intValue = defValue;
            if ((strValue == null) || (strValue.Length > 10))
            {
                return defValue;
            }
            bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
            if (IsFloat)
            {
                float.TryParse(strValue, out intValue);
            }
            return intValue;
        }

        /// <summary>
        /// string型转换为double型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的double类型结果</returns>
        public static double StrToDouble(string strValue, double defValue)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return defValue;
            }
            double intValue = defValue;

            if (Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
            {
                double.TryParse(strValue, out intValue);
            }
            return intValue;
        }

        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName)
        {
            return StrToInt(HttpContext.Current.Request.QueryString[strName], 0);
        }

        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName, int defValue)
        {
            return StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// 获得指定Url参数的long类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的long类型值</returns>
        public static long GetQueryLong(string strName, long defValue)
        {
            if (HttpContext.Current == null)
            {
                return defValue;
            }
            return StrToLong(HttpContext.Current.Request.QueryString[strName], defValue);
        }

        /// <summary>
        /// 获得指定表单参数的int类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的int类型值</returns>
        public static int GetFormInt(string strName, int defValue)
        {
            return StrToInt(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// 获得指定表单参数的long类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的long类型值</returns>
        public static long GetFormLong(string strName, long defValue)
        {
            return StrToLong(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static int GetInt(string strName, int defValue)
        {
            if (GetQueryInt(strName, defValue) == defValue)
            {
                return GetFormInt(strName, defValue);
            }
            return GetQueryInt(strName, defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的long类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的long类型值</returns>
        public static long GetLong(string strName, long defValue)
        {
            if (GetQueryLong(strName, defValue) == defValue)
            {
                return GetFormLong(strName, defValue);
            }
            return GetQueryLong(strName, defValue);
        }

        /// <summary>
        /// 获得指定Url参数的float类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static float GetQueryFloat(string strName, float defValue)
        {
            return StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// 获得指定表单参数的float类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的float类型值</returns>
        public static float GetFormFloat(string strName, float defValue)
        {
            return StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的float类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static float GetFloat(string strName, float defValue)
        {
            if (GetQueryFloat(strName, defValue) == defValue)
            {
                return GetFormFloat(strName, defValue);
            }
            return GetQueryFloat(strName, defValue);
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(string str)
        {
            return StrToDateTime(str, DateTime.Now);
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(string str, DateTime defValue)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime dateTime;
                if (DateTime.TryParse(str, out dateTime))
                {
                    return dateTime;
                }
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ObjectToDateTime(object obj)
        {
            return StrToDateTime(obj.ToString());
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ObjectToDateTime(object obj, DateTime defValue)
        {
            return StrToDateTime(obj.ToString(), defValue);
        }

        /// <summary>
        /// 获得指定Url参数的double类型值
        /// </summary>
        /// <param name="strName">Url参数</param> 
        /// <returns>Url参数的double类型值</returns>
        public static double GetQueryDouble(string strName)
        {
            return StrToDouble(HttpContext.Current.Request.QueryString[strName], 0);
        }

        /// <summary>
        /// 获得指定Url参数的double类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的double类型值</returns>
        public static double GetQueryDouble(string strName, double defValue)
        {
            return StrToDouble(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// 获得指定表单参数的double类型值
        /// </summary>
        /// <param name="strName">表单参数</param> 
        /// <returns>表单参数的double类型值</returns>
        public static double GetFormDouble(string strName)
        {
            return StrToDouble(HttpContext.Current.Request.Form[strName], 0);
        }
        /// <summary>
        /// 获得指定表单参数的double类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的double类型值</returns>
        public static double GetFormDouble(string strName, double defValue)
        {
            return StrToDouble(HttpContext.Current.Request.Form[strName], defValue);
        }

        /// <summary>
        /// 获得指定Url或表单参数的double类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的double类型值</returns>
        public static double GetDouble(string strName, double defValue)
        {
            if (GetQueryDouble(strName, defValue) == defValue)
            {
                return GetFormDouble(strName, defValue);
            }
            return GetQueryDouble(strName, defValue);
        }
    }
}
