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
            }
            return null;
        }

        internal static ICaptcha New(CaptchKind kind)
        {
            return GetCaptcha(kind);
        }
    }
}
