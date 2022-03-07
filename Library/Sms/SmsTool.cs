using CodeM.Common.Tools.Sms.Providers;
using System.Collections.Concurrent;

namespace CodeM.Common.Tools.Sms
{
    public class SmsTool
    {
        private static ConcurrentDictionary<SmsProvider, ISmsProvider> sProviders = new ConcurrentDictionary<SmsProvider, ISmsProvider>();

        internal static ISmsProvider GetProvider(SmsProvider _typ)
        {
            switch (_typ)
            {
                case SmsProvider.Alibaba:
                    return new AlibabaSms();
                case SmsProvider.Tencent:
                    break;
            }
            return null;
        }

        internal static ISmsProvider New(SmsProvider _typ)
        {
            if (!sProviders.TryGetValue(_typ, out ISmsProvider tool))
            {
                lock (sProviders)
                {
                    if (!sProviders.TryGetValue(_typ, out tool))
                    {
                        tool = GetProvider(_typ);
                        sProviders.TryAdd(_typ, tool);
                    }
                }
            }
            return tool;
        }
    }
}
