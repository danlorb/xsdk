using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using xSdk.Hosting;

namespace xSdk.Extensions.Variable
{
    public static class SerivceCollectionExtensions
    {
        private static List<Action<VariableServiceSetup>> _configureActions =
            new List<Action<VariableServiceSetup>>();

        public static IServiceCollection AddVariableServices(this IServiceCollection services) =>
            services.AddVariableServices(null);

        public static IServiceCollection AddVariableServices(
            this IServiceCollection services,
            Action<VariableServiceSetup>? configure
        )
        {
            if (configure != null)
            {
                _configureActions.Add(configure);
            }

            services.TryAddSingleton(provider =>
            {
                var service = SlimHost.Instance.VariableSystem;

                var setup = new VariableServiceSetup();
                foreach (var configureAction in _configureActions)
                {
                    configureAction?.Invoke(setup);
                }
                _configureActions.Clear();

                if (service is VariableService concreteService)
                {
                    foreach (var variableProvider in setup.Providers)
                    {
                        concreteService.RegisterProvider(variableProvider);
                    }

                    if (setup.AddEnvironmentVariablesWithoutSetup)
                    {
                        concreteService.AddEnvironmentVariables();
                    }

                    //if (setup.AddCommanlineVariablesWithoutSetup)
                    //{
                    //    concreteService.AddCommandlineVariables();
                    //}
                }

                return service;
            });

            return services;
        }

        internal static IServiceCollection AddSlimVariableServices(
            this IServiceCollection services,
            IConfigurationRoot config
        )
        {
            services.AddSingleton<IConfiguration>(config);
            services.TryAddSingleton<IVariableService>(provider =>
            {
                var service = ActivatorUtilities.CreateInstance<VariableService>(provider);

                // Add Environment Setup (it will always needed)
                service.RegisterSetup<EnvironmentSetup>();

                return service;
            });

            return services;
        }
    }
}
