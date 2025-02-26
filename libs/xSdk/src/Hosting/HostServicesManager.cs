using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using xSdk.Extensions.IO;
using xSdk.Extensions.Plugin;
using xSdk.Extensions.Variable;

namespace xSdk.Hosting
{
    public static class HostServicesManager
    {
        internal static void ConfigureHostServices(IServiceCollection services)
        {
            services.AddLogging(LoggingHelpers.ConfigureLogging).AddFileServices().AddPluginServices().AddVariableServices();

            SlimHostInternal.Instance.PluginSystem.Invoke<PluginBase>(x => x.ConfigureServices(services));
        }

        internal static void ConfigureHostServicesWithContext(HostBuilderContext context, IServiceCollection services)
        {
            SlimHostInternal.Instance.PluginSystem.Invoke<HostPluginBase>(x => x.ConfigureServices(context, services));
        }
    }
}
