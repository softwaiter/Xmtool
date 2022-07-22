namespace CodeM.Common.Tools
{
    public enum CaptchKind
    {
        Character,
        Sliding
    }

    public interface ICaptcha
    {
        /// <summary>
        /// 配置验证码对象的相关属性
        /// </summary>
        /// <param name="args">
        /// <para>CaptchKind.Character：字符长度(默认6)、只包含数字(默认true)、图片宽度(默认300)、图片高度(默认125)、图片背景色(默认Color.White)、图片边框色(默认Color.LightGray)6个参数；传入null即不设置，使用默认值</para>
        /// <para>CaptchKind.Sliding：图片资源目录（必填）</para>
        /// </param>
        /// <returns></returns>
        public ICaptcha Config(params object[] args);

        /// <summary>
        /// 返回生成好的验证码相关内容
        /// </summary>
        /// <param name="datas">
        /// <para>生成验证的可选参数</para>
        /// <para>CaptchKind.Character：验证码（String）</para>
        /// <para>CaptchKind.Sliding：无</para>
        /// </param>
        /// <returns>
        /// <para>CaptchKind.Character：验证码|验证码图片Base64字符串</para>
        /// <para>CaptchKind.Sliding：</para>
        /// </returns>
        public string Generate(params object[] datas);

        /// <summary>
        /// 校验验证码操作结果
        /// </summary>
        /// <param name="source">验证码原始生成数据</param>
        /// <param name="input">用户操作输入的数据</param>
        /// <returns></returns>
        public bool Validate(object source, object input);
    }
}
