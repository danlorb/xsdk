using Microsoft.AspNetCore.Builder;
using xSdk.Extensions.Plugin;

namespace xSdk.Extensions.Web
{
    public interface IApplicationPluginConfig : IPlugin
    {
        void Configure(IApplicationBuilder app);
    }
}
