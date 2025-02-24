using NLog;
using xSdk.Hosting;

namespace xSdk.Extensions.Plugin
{
    public abstract class PluginBuilderBase : IPluginBuilder
    {
        protected ILogger Logger { get; } = LogManager.GetCurrentClassLogger();
    }
}
