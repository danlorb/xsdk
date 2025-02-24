using Microsoft.Extensions.Hosting;
using xSdk.Hosting;

namespace xSdk.Plugins.Links
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder EnableLinks(this IHostBuilder hostBuilder)
        {
            return hostBuilder.EnablePlugin<LinksPlugin>();
        }
    }
}
