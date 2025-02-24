using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using xSdk.Extensions.Plugin;

namespace xSdk.Hosting
{
    public class HostPluginBase : PluginDescription
    {
        protected ILogger Logger { get; } = LogManager.GetCurrentClassLogger();

        public virtual void ConfigureServices(
            HostBuilderContext context,
            IServiceCollection services
        ) { }
    }
}
