using System.Text.RegularExpressions;

namespace CodeM.Common.Tools
{
    public class RegexUtils
    {
        #region 常用
        private static Regex reMobile = new Regex("^1\\d{10}$");
        private static Regex reEmail = new Regex("^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$");
        private static Regex reUrl = new Regex("^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$");
        private static Regex reIP = new Regex("^((2(5[0-5]|[0-4]\\d))|[0-1]?\\d{1,2})(\\.((2(5[0-5]|[0-4]\\d))|[0-1]?\\d{1,2})){3}$");

        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsMobile(string value)
        {
            return reMobile.IsMatch(value);
        }

        /// <summary>
        /// 是否电子邮箱
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmail(string value)
        {
            return reEmail.IsMatch(value);
        }

        /// <summary>
        /// 是否网址
        /// </summary>
        /// <param name="vaule"></param>
        /// <returns></returns>
        public static bool IsUrl(string vaule)
        {
            return reUrl.IsMatch(vaule);
        }

        /// <summary>
        /// 是否IPv4地址
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIP(string value)
        {
            return reIP.IsMatch(value);
        }
        #endregion

        #region 数字相关
        private static Regex reNumber = new Regex("^-?\\d+$|^-?\\d+\\.\\d+$");
        private static Regex reInt = new Regex("^-?\\d+$");
        private static Regex reNaturalInt = new Regex("^\\d+$");
        private static Regex rePositiveInt = new Regex("^[1-9][0-9]*$");
        private static Regex reDecimal = new Regex("^-?\\d+\\.\\d+$");

        /// <summary>
        /// 是否数值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumber(string value)
        {
            return reNumber.IsMatch(value);
        }

        /// <summary>
        /// 是否整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInteger(string value)
        {
            return reInt.IsMatch(value);
        }

        /// <summary>
        /// 是否正整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPositiveInteger(string value)
        {
            return rePositiveInt.IsMatch(value);
        }

        /// <summary>
        /// 是否自然数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNaturalInteger(string value)
        {
            return reNaturalInt.IsMatch(value);
        }

        /// <summary>
        /// 是否小数，必须包含小数点部分
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDecimal(string value)
        {
            return reDecimal.IsMatch(value);
        }
        #endregion
    }
}
