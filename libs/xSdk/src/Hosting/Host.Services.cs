using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using xSdk.Extensions.IO;
using xSdk.Extensions.Plugin;
using xSdk.Extensions.Variable;

namespace xSdk.Hosting
{
    public static partial class Host
    {
        private static void ConfigureHostServices(IServiceCollection services)
        {
            services.AddLogging(LoggingHelpers.ConfigureLogging).AddFileServices().AddVariableServices();

            SlimHostInternal.Instance.PluginSystem.Invoke<PluginBase>(x => x.ConfigureServices(services));
        }

        private static void ConfigureHostServicesWithContext(HostBuilderContext context, IServiceCollection services)
        {
            SlimHostInternal.Instance.PluginSystem.Invoke<HostPluginBase>(x => x.ConfigureServices(context, services));
        }
    }
}
