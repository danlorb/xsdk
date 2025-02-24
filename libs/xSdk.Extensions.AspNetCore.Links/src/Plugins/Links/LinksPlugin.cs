using Microsoft.Extensions.DependencyInjection;
using xSdk.Extensions.Plugin;
using xSdk.Hosting;

namespace xSdk.Plugins.Links
{
    internal class LinksPlugin : PluginBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            //services.AddLinks(config =>
            //{
            //    SlimHost.Instance.PluginSystem.Invoke<ILinksPluginConfig>(x => x.ConfigureLinks(config));
            //});
        }
    }
}
