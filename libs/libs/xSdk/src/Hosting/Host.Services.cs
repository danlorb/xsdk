using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using xSdk.Extensions.Plugin;
using xSdk.Extensions.Variable;

namespace xSdk.Hosting
{
    public static partial class Host
    {
        internal static bool HostServicesDisabled = false;

        public static void ConfigureHostDefaultServices(IServiceCollection services)
        {
            services.AddLogging(LoggingHelpers.ConfigureLogging).AddVariableServices();
        }

        private static void ConfigureHostServices(
            HostBuilderContext context,
            IServiceCollection services
        )
        {
            if (!HostServicesDisabled)
            {
                ConfigureHostDefaultServices(services);

                SlimHost.Instance.PluginSystem.Invoke<IServicesPluginConfig>(x =>
                    x.ConfigureServices(services)
                );
                SlimHost.Instance.PluginSystem.Invoke<IHostServicesPluginConfig>(x =>
                    x.ConfigureServices(context, services)
                );
            }
        }
    }
}
