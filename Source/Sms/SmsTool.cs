using CodeM.Common.Tools.Sms.Providers;

namespace CodeM.Common.Tools.Sms
{
    public class SmsTool
    {
        internal static ISmsProvider GetProvider(SmsProvider _typ)
        {
            switch (_typ)
            {
                case SmsProvider.Alibaba:
                    return new AlibabaSms();
                case SmsProvider.Tencent:
                    return new TencentSms();
            }
            return null;
        }

        internal static ISmsProvider New(SmsProvider _typ)
        {
            return GetProvider(_typ);
        }
    }
}
