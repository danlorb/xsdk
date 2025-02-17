using Microsoft.AspNetCore.Builder;
using xSdk.Extensions.Plugin;

namespace xSdk.Extensions.Web
{
    public interface IApplicationPreparePluginConfig : IPlugin
    {
        void Configure(IApplicationBuilder app);
    }
}
