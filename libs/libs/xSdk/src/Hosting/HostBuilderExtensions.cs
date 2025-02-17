using System.Reflection;
using Microsoft.Extensions.Hosting;
using xSdk.Extensions.Plugin;
using xSdk.Extensions.Variable;

namespace xSdk.Hosting
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder DisableHostServiceLoading(this IHostBuilder hostBuilder)
        {
            // Disables ConfigureServices from local HostBuilder
            // This is needed for WebHostBuilder to work probably
            // If you dont do this, Web Controllers will not be registered
            Host.HostServicesDisabled = true;

            return hostBuilder;
        }

        public static IHostBuilder ConfigurePlugin<TPlugin>(this IHostBuilder builder)
            where TPlugin : IPlugin => builder.AddPlugin<TPlugin>();

        public static IHostBuilder AddPlugin(this IHostBuilder builder, Type pluginType)
        {
            SlimHost.Instance.PluginSystem.AddPlugin(pluginType);
            return builder;
        }

        public static IHostBuilder AddPlugin<TPlugin>(this IHostBuilder builder)
            where TPlugin : IPlugin
        {
            SlimHost.Instance.PluginSystem.AddPlugin<TPlugin>();
            return builder;
        }

        public static IHostBuilder AddPluginsFrom<TSource>(this IHostBuilder builder)
        {
            SlimHost.Instance.PluginSystem.AddPluginsFrom<TSource>();
            return builder;
        }

        public static IHostBuilder AddPluginsFrom(
            this IHostBuilder builder,
            Assembly sourceAssembly
        )
        {
            SlimHost.Instance.PluginSystem.AddPluginsFrom(sourceAssembly);
            return builder;
        }

        public static IHostBuilder RegisterSetup<TSetup>(this IHostBuilder builder)
            where TSetup : class, ISetup, new()
        {
            SlimHost.Instance.VariableSystem.RegisterSetup<TSetup>();
            return builder;
        }

        public static IHostBuilder RegisterSetup<TSetup>(
            this IHostBuilder builder,
            Action<TSetup>? configure
        )
            where TSetup : class, ISetup, new()
        {
            SlimHost.Instance.VariableSystem.RegisterSetup<TSetup>(configure);
            return builder;
        }

        public static IHostBuilder RegisterSetup<TSetup>(
            this IHostBuilder builder,
            TSetup implementation
        )
            where TSetup : class, ISetup, new()
        {
            SlimHost.Instance.VariableSystem.RegisterSetup<TSetup>(implementation);
            return builder;
        }
    }
}
