using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using xSdk.Extensions.Plugin;

namespace xSdk.Hosting
{
    public interface IHostServicesPluginConfig : IPlugin
    {
        void ConfigureServices(HostBuilderContext context, IServiceCollection services);
    }
}
