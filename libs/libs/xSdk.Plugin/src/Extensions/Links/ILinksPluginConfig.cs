using Microsoft.AspNetCore.Routing;
using xSdk.Extensions.Plugin;

namespace xSdk.Extensions.Links
{
    public interface ILinksPluginConfig : IPlugin
    {
        void ConfigureLinks(LinkOptions options);
    }
}
