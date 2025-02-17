using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using xSdk.Hosting;

namespace xSdk.Extensions.Plugin
{
    public static class ServiceCollectionExtensions
    {
        private static List<Action<PluginBuilder>> _configureActions =
            new List<Action<PluginBuilder>>();

        public static IServiceCollection AddPluginServices(this IServiceCollection services) =>
            AddPluginServices(services, null);

        public static IServiceCollection AddPluginServices(
            this IServiceCollection services,
            Action<PluginBuilder>? configure
        )
        {
            if (configure != null)
            {
                _configureActions.Add(configure);
            }

            services.TryAddSingleton<IPluginService>(provider =>
            {
                var builder = new PluginBuilder();
                foreach (var configure in _configureActions)
                {
                    configure?.Invoke(builder);
                }

                var service = SlimHost.Instance.PluginSystem;
                if (service is PluginService concreteService)
                {
                    concreteService.Initialize(builder);
                }

                return service;
            });

            return services;
        }

        internal static IServiceCollection AddSlimPluginServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IPluginService>(provider =>
            {
                var service = ActivatorUtilities.CreateInstance<PluginService>(provider);
                return service;
            });

            return services;
        }
    }
}
