using Microsoft.Extensions.DependencyInjection;
using xSdk.Extensions.Plugin;

namespace xSdk.Hosting
{
    public interface IServicesPluginConfig : IPlugin
    {
        void ConfigureServices(IServiceCollection services);
    }
}
