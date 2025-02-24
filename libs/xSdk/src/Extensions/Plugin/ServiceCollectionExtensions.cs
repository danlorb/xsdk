using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using xSdk.Hosting;

namespace xSdk.Extensions.Plugin
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPluginServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IPluginService>(SlimHostInternal.Instance.PluginSystem);

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
