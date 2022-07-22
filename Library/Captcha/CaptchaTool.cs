using CodeM.Common.Tools.Captcha.Implements;

namespace CodeM.Common.Tools.Captcha
{
    public class CaptchaTool
    {
        internal static ICaptcha GetCaptcha(CaptchKind kind)
        {
            switch (kind)
            {
                case CaptchKind.Character:
                    return new CharacterCaptcha();
                case CaptchKind.Sliding:
                    return new SlidingCaptcha();
            }
            return null;
        }

        internal static ICaptcha New(CaptchKind kind)
        {
            return GetCaptcha(kind);
        }
    }
}
