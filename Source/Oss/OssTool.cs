using CodeM.Common.Tools.Oss.Providers;

namespace CodeM.Common.Tools.Oss
{
    public class OssTool
    {
        internal static IOssProvider GetProvider(OssProvider _typ)
        {
            switch (_typ)
            {
                case OssProvider.Qiniu:
                    return new QiniuOss();
                case OssProvider.Alibaba:
                    return new AlibabaOss();
                case OssProvider.Tencent:
                    return new TencentOss();
            }
            return null;
        }

        internal static IOssProvider New(OssProvider _typ)
        {
            return GetProvider(_typ);
        }
    }
}
