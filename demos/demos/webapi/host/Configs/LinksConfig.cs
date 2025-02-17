using Microsoft.AspNetCore.Routing;
using xSdk.Demos.Data;
using xSdk.Extensions.Links;
using xSdk.Extensions.Plugin;

namespace xSdk.Demos.Configs
{
    internal class LinksConfig : PluginBase, ILinksPluginConfig
    {
        public void ConfigureLinks(LinkOptions options)
        {
            //options
            //    .AddPolicy<SampleModelLinks>(x => x.For<SampleModel>());
        }
    }
}
