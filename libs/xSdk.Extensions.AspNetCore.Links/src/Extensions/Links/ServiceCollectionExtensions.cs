using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace xSdk.Extensions.Links
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLinks(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            //services.TryAddSingleton<IAssemblyLoader, DefaultAssemblyLoader>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.TryAddSingleton<IRouteMap, DefaultRouteMap>();
            //services.TryAdd(ServiceDescriptor.Transient<ILinksHandlerContextFactory, DefaultLinksHandlerContextFactory>());
            services.TryAdd(
                ServiceDescriptor.Transient<ILinksPolicyProvider, DefaultLinksPolicyProvider>()
            );
            services.TryAdd(
                ServiceDescriptor.Transient<
                    ILinkTransformationContextFactory,
                    DefaultLinkTransformationContextFactory
                >()
            );
            services.TryAdd(ServiceDescriptor.Transient<ILinksEvaluator, DefaultLinksEvaluator>());
            //services.TryAdd(ServiceDescriptor.Transient<ILinkAuthorizationService, DefaultLinkAuthorizationService>());
            services.TryAdd(ServiceDescriptor.Transient<ILinksService, DefaultLinksService>());
            //services.TryAddEnumerable(ServiceDescriptor.Transient<ILinksHandler, Implementation.PassThroughLinksHandler>());
            return services;
        }

        public static IServiceCollection AddLinks(
            this IServiceCollection services,
            Action<LinksOptions> configure
        )
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            services.Configure(configure);
            return services.AddLinks();
            ;
        }
    }
}
