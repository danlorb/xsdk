using Microsoft.AspNetCore.DataProtection;
using xSdk.Extensions.Plugin;

namespace xSdk.Extensions.DataProtection
{
    public interface IDataProtectionPluginConfig : IPlugin
    {
        void ConfigureDataProtection(IDataProtectionBuilder builder);
    }
}
