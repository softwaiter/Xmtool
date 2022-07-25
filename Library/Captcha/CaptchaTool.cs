using CodeM.Common.Tools.Captcha.Implements;

namespace CodeM.Common.Tools.Captcha
{
    public class CaptchaTool
    {
        internal static ICaptcha GetCaptcha(CaptchaKind kind)
        {
            switch (kind)
            {
                case CaptchaKind.Character:
                    return new CharacterCaptcha();
                case CaptchaKind.Sliding:
                    return new SlidingCaptcha();
            }
            return null;
        }

        internal static ICaptcha New(CaptchaKind kind)
        {
            return GetCaptcha(kind);
        }
    }
}
