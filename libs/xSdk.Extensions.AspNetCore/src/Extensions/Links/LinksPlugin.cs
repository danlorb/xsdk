using Microsoft.Extensions.DependencyInjection;
using RiskFirst.Hateoas;
using xSdk.Extensions.Plugin;
using xSdk.Hosting;

namespace xSdk.Extensions.Links
{
    internal class LinksPlugin : PluginBase, IServicesPluginConfig
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddLinks(config =>
            //{
            //    SlimHost.Instance.PluginSystem.Invoke<ILinksPluginConfig>(x => x.ConfigureLinks(config));
            //});
        }
    }
}
