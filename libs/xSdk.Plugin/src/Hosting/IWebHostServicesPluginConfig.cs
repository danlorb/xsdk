using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using xSdk.Extensions.Plugin;

namespace xSdk.Hosting
{
    public interface IWebHostServicesPluginConfig : IPlugin
    {
        void ConfigureServices(WebHostBuilderContext context, IServiceCollection services);
    }
}
