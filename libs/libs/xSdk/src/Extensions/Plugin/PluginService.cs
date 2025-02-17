using System.Reflection;
using Microsoft.Extensions.Logging;
using Weikio.PluginFramework.Catalogs;
using xSdk.Extensions.IO;

namespace xSdk.Extensions.Plugin
{
    internal class PluginService(IFileSystemService fsService, ILogger<PluginService> logger)
        : IPluginService
    {
        private CompositePluginCatalog aggregateCatalog = new();

        private List<IPlugin> _plugins = new List<IPlugin>();

        internal void Initialize(PluginBuilder? builder)
        {
            if (builder == null)
            {
                return;
            }

            var machineCatalog = CatalogHelper.CreateFolderPluginCatalog(
                fsService,
                FileSystemContext.Machine,
                "/plugins"
            );
            var userCatalog = CatalogHelper.CreateFolderPluginCatalog(
                fsService,
                FileSystemContext.User,
                "/plugins"
            );
            var localCatalog = CatalogHelper.CreateFolderPluginCatalog(
                fsService,
                FileSystemContext.Local,
                "/plugins"
            );

            if (machineCatalog != null)
            {
                aggregateCatalog.AddCatalog(machineCatalog);
            }

            if (userCatalog != null)
            {
                aggregateCatalog.AddCatalog(userCatalog);
            }

            if (localCatalog != null)
            {
                aggregateCatalog.AddCatalog(localCatalog);
            }

            foreach (var catalog in builder._catalogs)
            {
                aggregateCatalog.AddCatalog(catalog);
            }
        }

        public async Task<TPlugin?> GetPluginAsync<TPlugin>(CancellationToken token = default)
            where TPlugin : IPlugin
        {
            var result = await GetPluginsAsync<TPlugin>(token);

            return result.FirstOrDefault();
        }

        public async Task<IList<TPlugin>> GetPluginsAsync<TPlugin>(
            CancellationToken token = default
        )
            where TPlugin : IPlugin
        {
            if (!TryGetLoadedPlugins(out List<TPlugin> result))
            {
                await aggregateCatalog.Initialize();

                var abstractPlugins = aggregateCatalog.GetPlugins().Where(x => x != null);

                foreach (var abstractPlugin in abstractPlugins)
                {
                    try
                    {
                        var concretePlugin = Activator.CreateInstance(abstractPlugin);
                        if (concretePlugin is PluginBase pluginInstance)
                        {
                            pluginInstance.Initialize(abstractPlugin);
                        }

                        if (concretePlugin is TPlugin typedPlugin)
                        {
                            result.Add(typedPlugin);
                            _plugins.Add(typedPlugin);
                        }
                    }
                    catch (MissingMethodException mme)
                    {
                        // Ignore this type of Exception, because this Exception is thrown
                        // if a class does not have a default constructor.
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Failed to create plugin instance");
                    }
                }
            }

            return result;
        }

        public Task AddPluginAsync(Type pluginType, CancellationToken token = default)
        {
            aggregateCatalog.AddCatalog(CatalogHelper.CreateTypeCatalog(pluginType));
            return Task.CompletedTask;
        }

        public Task AddPluginsFromAsync(Assembly sourceAssembly, CancellationToken token = default)
        {
            aggregateCatalog.AddCatalog(CatalogHelper.CreateAssemblyCatalog(sourceAssembly));
            return Task.CompletedTask;
        }

        private bool TryGetLoadedPlugins<TPlugin>(out List<TPlugin> plugins)
            where TPlugin : IPlugin
        {
            plugins = new List<TPlugin>();
            var result = _plugins.Where(x => x.GetType().IsAssignableTo(typeof(TPlugin)));

            if (result != null && result.Any())
            {
                plugins = result.Cast<TPlugin>().ToList();
                return true;
            }

            return false;
        }
    }
}
