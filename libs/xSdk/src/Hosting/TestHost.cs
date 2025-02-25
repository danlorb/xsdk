using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;

namespace xSdk.Hosting
{
    public static partial class TestHost
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private const string APP_NAME = "xUnitTestHost";
        private const string APP_COMPANY = "xUnit";
        private const string APP_PREFIX = "UnitTest";

        public static IHostBuilder CreateBuilder() => CreateBuilder(new string[] { }, APP_NAME, APP_COMPANY, APP_PREFIX);

        public static IHostBuilder CreateBuilder(string[] args) => CreateBuilder(args, APP_NAME, APP_COMPANY, APP_PREFIX);

        public static IHostBuilder CreateBuilder(string[] args, string appName) => CreateBuilder(args, appName, APP_COMPANY, APP_PREFIX);

        public static IHostBuilder CreateBuilder(string[] args, string appName, string appPrefix) => CreateBuilder(args, appName, APP_COMPANY, appPrefix);

        public static IHostBuilder CreateBuilder(string[] args, string? appName, string? appCompany, string? appPrefix)
        {
            var boot = SlimHostInternal.InitializeTestHost(args, appName, appCompany, appPrefix);

            var builder = new HostBuilder();

            return builder;
        }
    }
}
