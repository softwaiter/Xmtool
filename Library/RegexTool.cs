using System.Text.RegularExpressions;

namespace CodeM.Common.Tools
{
    public class RegexTool
    {
        private static RegexTool sRTool = new RegexTool();

        private RegexTool()
        { 
        }

        internal static RegexTool New()
        {
            return sRTool;
        }

        #region 常用
        private static Regex reMobile = new Regex("^1\\d{10}$");
        private static Regex reTelephone = new Regex("^0((\\d{2}-\\d{8})|(\\d{3}-\\d{7}))$");
        private static Regex reEmail = new Regex("^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$");
        private static Regex reUrl = new Regex("^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$");
        private static Regex reIP = new Regex("^((2(5[0-5]|[0-4]\\d))|[0-1]?\\d{1,2})(\\.((2(5[0-5]|[0-4]\\d))|[0-1]?\\d{1,2})){3}$");
        private static Regex reIDCard = new Regex("^d{6}(18|19|20)d{2}(0d|11|12)([0-2]d|30|31)d{3}(d|X|x)$");

        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsMobile(string value)
        {
            return reMobile.IsMatch(value);
        }

        /// <summary>
        /// 是否固定电话号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsTelephone(string value)
        {
            return reTelephone.IsMatch(value);
        }

        /// <summary>
        /// 是否电子邮箱
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsEmail(string value)
        {
            return reEmail.IsMatch(value);
        }

        /// <summary>
        /// 是否网址
        /// </summary>
        /// <param name="vaule"></param>
        /// <returns></returns>
        public bool IsUrl(string vaule)
        {
            return reUrl.IsMatch(vaule);
        }

        /// <summary>
        /// 是否IPv4地址
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsIP(string value)
        {
            return reIP.IsMatch(value);
        }

        /// <summary>
        /// 是否身份证号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsIDCard(string value)
        {
            return reIDCard.IsMatch(value);
        }
        #endregion

        #region 字符相关

        private Regex reEnglish = new Regex("^[a-zA-Z]+$");
        private Regex reLowercaseEnglish = new Regex("^[a-z]+$");
        private Regex reCapitalEnglish = new Regex("^[A-Z]+$");
        private Regex reChinese = new Regex("^[一-龥]+$");
        private Regex reMixChar = new Regex("^[A-Za-z0-9]+$");
        private Regex reMixChar2 = new Regex("^(([一-龥])|[A-Za-z0-9])+$");

        /// <summary>
        /// 是否英文字符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsEnglish(string value)
        {
            return reEnglish.IsMatch(value);
        }

        /// <summary>
        /// 是否小写英文
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsLowercaseEnglish(string value)
        {
            return reLowercaseEnglish.IsMatch(value);
        }

        /// <summary>
        /// 是否大写英文
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsCapitalEnglish(string value)
        {
            return reCapitalEnglish.IsMatch(value);
        }

        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsChinese(string value)
        {
            return reChinese.IsMatch(value);
        }

        /// <summary>
        /// 是否只包含数字或英文
        /// </summary>
        /// <returns></returns>
        public bool IsMixChar(string value)
        {
            return reMixChar.IsMatch(value);
        }

        /// <summary>
        /// 是否只包含中文、数字或英文
        /// </summary>
        /// <returns></returns>
        public bool IsMixChar2(string value)
        { 
            return reMixChar2.IsMatch(value);
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
        public bool IsNumber(string value)
        {
            return reNumber.IsMatch(value);
        }

        /// <summary>
        /// 是否整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsInteger(string value)
        {
            return reInt.IsMatch(value);
        }

        /// <summary>
        /// 是否正整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsPositiveInteger(string value)
        {
            return rePositiveInt.IsMatch(value);
        }

        /// <summary>
        /// 是否自然数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsNaturalInteger(string value)
        {
            return reNaturalInt.IsMatch(value);
        }

        /// <summary>
        /// 是否小数，必须包含小数点部分
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsDecimal(string value)
        {
            return reDecimal.IsMatch(value);
        }
        #endregion
    }
}
