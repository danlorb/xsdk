using Microsoft.Extensions.Hosting;
using xSdk.Extensions.Plugin;

namespace xSdk.Hosting
{
    public interface IWebHostBuilderPluginConfig : IPlugin
    {
        void ConfigureWebHost(IHostBuilder builder);
    }
}
