namespace xSdk.Extensions.Plugin
{
    public static class PluginExtensions
    {
        public static bool Invoke<TPlugin>(
            this IPluginService pluginService,
            Action<TPlugin> factory
        )
            where TPlugin : IPlugin
        {
            var plugins = pluginService.GetPlugins<TPlugin>();
            foreach (var plugin in plugins)
            {
                factory?.Invoke(plugin);
            }

            return plugins.Any();
        }

        public static bool Exists<TPlugin>(this IPluginService pluginService)
            where TPlugin : IPlugin
        {
            var plugins = pluginService.GetPlugins<TPlugin>();
            return plugins.Any();
        }
    }
}
