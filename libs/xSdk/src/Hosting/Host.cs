using Microsoft.Extensions.Hosting;
using NLog;
using xSdk.Extensions.Plugin;

namespace xSdk.Hosting
{
    public static partial class Host
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static IHostBuilder CreateBuilder(string[] args) => CreateBuilder(args, default, default, default);

        public static IHostBuilder CreateBuilder(string[] args, string appName) => CreateBuilder(args, appName, default, default);

        public static IHostBuilder CreateBuilder(string[] args, string appName, string appPrefix) => CreateBuilder(args, appName, default, appPrefix);

        public static IHostBuilder CreateBuilder(string[] args, string? appName, string? appCompany, string? appPrefix)
        {
            var boot = SlimHostInternal.Initialize(args, appName, appCompany, appPrefix);

            var builder = new HostBuilder()
                .ConfigureHostConfiguration(HostConfigurationManager.LoadHostConfiguration)
                .ConfigureAppConfiguration(HostConfigurationManager.LoadAppConfiguration)
                .ConfigureServices(HostServicesManager.ConfigureHostServices)
                .ConfigureServices(HostServicesManager.ConfigureHostServicesWithContext);

            // Shutdown the logger
            AppDomain.CurrentDomain.ProcessExit += (sender, args) =>
            {
                LogManager.Flush();
                LogManager.Shutdown();
            };

            return builder;
        }
    }
}
