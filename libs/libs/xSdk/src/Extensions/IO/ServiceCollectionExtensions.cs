using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using xSdk.Extensions.Variable;
using xSdk.Hosting;

namespace xSdk.Extensions.IO
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFileServices(this IServiceCollection services)
        {
            services.TryAddSingleton(provider => SlimHost.Instance.FileSystem);

            return services;
        }

        internal static IServiceCollection AddSlimFileServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IFileSystemService, FileSystemService>();

            return services;
        }
    }
}
