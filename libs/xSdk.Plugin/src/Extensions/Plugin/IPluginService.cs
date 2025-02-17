using System.Reflection;

namespace xSdk.Extensions.Plugin
{
    public interface IPluginService
    {
        TPlugin? GetPlugin<TPlugin>()
            where TPlugin : IPlugin =>
            GetPluginAsync<TPlugin>().ConfigureAwait(false).GetAwaiter().GetResult();

        Task<TPlugin?> GetPluginAsync<TPlugin>(CancellationToken token = default)
            where TPlugin : IPlugin;

        IList<TPlugin> GetPlugins<TPlugin>()
            where TPlugin : IPlugin =>
            GetPluginsAsync<TPlugin>().ConfigureAwait(false).GetAwaiter().GetResult();

        Task<IList<TPlugin>> GetPluginsAsync<TPlugin>(CancellationToken token = default)
            where TPlugin : IPlugin;

        IList<IPlugin> GetPlugins() =>
            GetPluginsAsync().ConfigureAwait(false).GetAwaiter().GetResult();

        Task<IList<IPlugin>> GetPluginsAsync(CancellationToken token = default) =>
            GetPluginsAsync<IPlugin>(token);

        void AddPlugin(Type pluginType) =>
            AddPluginAsync(pluginType).ConfigureAwait(false).GetAwaiter().GetResult();

        Task AddPluginAsync(Type pluginType, CancellationToken token = default);

        void AddPlugin<TPlugin>()
            where TPlugin : IPlugin =>
            AddPluginAsync<TPlugin>().ConfigureAwait(false).GetAwaiter().GetResult();

        Task AddPluginAsync<TPlugin>(CancellationToken token = default)
            where TPlugin : IPlugin => AddPluginAsync(typeof(TPlugin), token);

        void AddPluginsFrom<TSource>() =>
            AddPluginsFromAsync<TSource>().ConfigureAwait(false).GetAwaiter().GetResult();

        Task AddPluginsFromAsync<TSource>(CancellationToken token = default) =>
            AddPluginsFromAsync(typeof(TSource).Assembly, token);

        void AddPluginsFrom(Assembly sourceAssembly) =>
            AddPluginsFromAsync(sourceAssembly).ConfigureAwait(false).GetAwaiter().GetResult();

        Task AddPluginsFromAsync(Assembly sourceAssembly, CancellationToken token = default);
    }
}
