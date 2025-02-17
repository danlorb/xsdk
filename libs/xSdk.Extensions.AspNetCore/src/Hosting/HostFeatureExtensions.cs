using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using NLog;
using xSdk.Extensions.Authentication;
using xSdk.Extensions.Compression;
using xSdk.Extensions.DataProtection;
using xSdk.Extensions.Documentation;
using xSdk.Extensions.Links;
using xSdk.Extensions.Plugin;
using xSdk.Extensions.Web;
using xSdk.Plugins;

namespace xSdk.Hosting
{
    public static class HostFeatureExtensions
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static IHostBuilder EnableWebApi(this IHostBuilder hostBuilder)
        {
            logger.Info("Prepare host for WebApi");

            hostBuilder
                .DisableHostServiceLoading()
                .RegisterSetup<WebSetup>()
                .AddPlugin<WebHostPlugin>()
                .AddPlugin<WebApiPlugin>();

            SlimHost.Instance.PluginSystem.Invoke<IWebHostBuilderPluginConfig>(x =>
                x.ConfigureWebHost(hostBuilder)
            );

            return hostBuilder;
        }

        public static IHostBuilder EnableAuthentication(this IHostBuilder hostBuilder)
        {
            hostBuilder.RegisterSetup<ApiKeySetup>().AddPlugin<AuthenticationPlugin>();

            return hostBuilder;
        }

        public static IHostBuilder EnableCompression(this IHostBuilder hostBuilder)
        {
            return hostBuilder.AddPlugin<CompressionPlugin>();
        }

        public static IHostBuilder EnableDataProtection(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .RegisterSetup<DataProtectionSetup>()
                .AddPlugin<DataProtectionPlugin>();
        }

        public static IHostBuilder EnableWebSecurity(this IHostBuilder hostBuilder)
        {
            return hostBuilder.RegisterSetup<WebSecuritySetup>().AddPlugin<WebSecurityPlugin>();
        }

        public static IHostBuilder EnableDocumentation(this IHostBuilder hostBuilder)
        {
            return hostBuilder.RegisterSetup<DocumentationSetup>().AddPlugin<DocumentationPlugin>();
        }

        public static IHostBuilder EnableLinks(this IHostBuilder hostBuilder)
        {
            return hostBuilder.AddPlugin<LinksPlugin>();
        }
    }
}
