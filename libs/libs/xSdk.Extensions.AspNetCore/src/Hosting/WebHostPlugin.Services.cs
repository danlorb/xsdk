using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using xSdk.Extensions.Plugin;

namespace xSdk.Hosting
{
    internal sealed partial class WebHostPlugin
    {
        private static void ConfigureWebHostServices(
            WebHostBuilderContext context,
            IServiceCollection services
        )
        {
            SlimHost.Instance.PluginSystem.Invoke<IServicesPluginConfig>(x =>
                x.ConfigureServices(services)
            );
            SlimHost.Instance.PluginSystem.Invoke<IWebHostServicesPluginConfig>(x =>
                x.ConfigureServices(context, services)
            );
        }
    }
}
