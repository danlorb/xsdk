using NLog;

namespace xSdk.Extensions.Plugin
{
    public abstract class PluginBase : IPlugin
    {
        public string? Name { get; internal set; }

        public Version? Version { get; internal set; }

        //public string? Description { get; internal set; }

        //public string? ProductVersion { get; internal set; }

        //public string? Tag { get; internal set; }

        //public Type? Type { get; internal set; }

        //public List<string> Tags { get; internal set; } = new List<string>();

        public void Initialize(Weikio.PluginFramework.Abstractions.Plugin? plugin)
        {
            if (plugin != null)
            {
                Logger.Info($"Initializing plugin {plugin.Name} v{plugin.Version}");
                Name = plugin.Name;
                Version = plugin.Version;
                //ProductVersion = plugin.ProductVersion;
                //Description = plugin.Description;
                //Tag = plugin.Tag;
                //Tags = plugin.Tags;
                //Type = plugin.Type;
            }
        }

        protected ILogger Logger { get; } = LogManager.GetCurrentClassLogger();
    }
}
