using Microsoft.Extensions.DependencyInjection;
using NLog;
using xSdk.Extensions.Plugin;

namespace xSdk.Hosting
{
    public abstract class PluginBase : PluginDescription, IPlugin
    {
        public virtual void ConfigureServices(IServiceCollection services) { }
    }
}
