using Microsoft.AspNetCore.Routing;
using xSdk.Extensions.Plugin;

namespace xSdk.Extensions.Web
{
    public interface IEndpointPluginConfig : IPlugin
    {
        void ConfigureEndpoint(IEndpointRouteBuilder builder);
    }
}
