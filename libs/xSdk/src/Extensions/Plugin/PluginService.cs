using System.Reflection;
using Microsoft.Extensions.Logging;
using Weikio.PluginFramework.Catalogs;
using xSdk.Extensions.IO;
using xSdk.Hosting;
using xSdk.Shared;

namespace xSdk.Extensions.Plugin
{
    internal class PluginService(IFileSystemService fsService, ILogger<PluginService> logger)
        : IPluginService
    {
        private CompositePluginCatalog aggregateCatalog;
        private readonly Dictionary<Type, TypePluginCatalog> typePluginCatalogs = new();
        private bool isTypePluginCatalogsStale = false;

        private readonly Dictionary<Assembly, AssemblyPluginCatalog> assemblyPluginCatalogs = new();
        private bool isAssemblyPluginCatalogsStale = false;

        private readonly List<object> _plugins = new();

        public Task<TPlugin?> GetPluginAsync<TPlugin>(CancellationToken token = default) =>
            GetPluginsAsync<TPlugin>(token).ContinueWith(task => task.Result.FirstOrDefault());

        public async Task<IList<TPlugin>> GetPluginsAsync<TPlugin>(
            CancellationToken token = default
        )
        {
            if (!TryGetLoadedPlugins(_plugins, out List<TPlugin> result))
            {
                var abstractPlugins = await LoadPluginsAsync();
                foreach (var item in abstractPlugins)
                {
                    if (item.Item1 is TPlugin concretePlugin)
                    {
                        InitializePlugin(concretePlugin, item.Item2);

                        result.Add(concretePlugin);
                        _plugins.Add(concretePlugin);
                    }
                }
            }

            return result;
        }

        public Task EnablePluginAsync<TPlugin>(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task DisablePluginAsync<TPlugin>(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task AddPluginAsync(Type pluginType, CancellationToken token = default)
        {
            typePluginCatalogs.AddOrNew(pluginType, CatalogHelper.CreateTypeCatalog(pluginType));
            isTypePluginCatalogsStale = true;

            return Task.CompletedTask;
        }

        public Task AddPluginsFromAsync(Assembly sourceAssembly, CancellationToken token = default)
        {
            assemblyPluginCatalogs.AddOrNew(
                sourceAssembly,
                CatalogHelper.CreateAssemblyCatalog(sourceAssembly)
            );
            isAssemblyPluginCatalogsStale = true;

            return Task.CompletedTask;
        }

        public Task RemovePluginAsync(Type pluginType, CancellationToken token = default)
        {
            if (typePluginCatalogs.ContainsKey(pluginType))
            {
                isTypePluginCatalogsStale = true;
                typePluginCatalogs.Remove(pluginType);
            }
            return Task.CompletedTask;
        }

        public Task RemovePluginsFromAsync(
            Assembly sourceAssembly,
            CancellationToken token = default
        )
        {
            if (assemblyPluginCatalogs.ContainsKey(sourceAssembly))
            {
                isAssemblyPluginCatalogsStale = true;
                assemblyPluginCatalogs.Remove(sourceAssembly);
            }
            return Task.CompletedTask;
        }

        private async Task<
            List<Tuple<object?, Weikio.PluginFramework.Abstractions.Plugin>>
        > LoadPluginsAsync()
        {
            List<Tuple<object?, Weikio.PluginFramework.Abstractions.Plugin>> plugins = new();

            var catalog = await InitialzeCatalogsAsync();

            var abstractPlugins = catalog.GetPlugins().Where(x => x != null);
            foreach (var abstractPlugin in abstractPlugins)
            {
                try
                {
                    Tuple<object?, Weikio.PluginFramework.Abstractions.Plugin> item = new(
                        Activator.CreateInstance(abstractPlugin),
                        abstractPlugin
                    );
                    plugins.Add(item);
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

            return plugins;
        }

        private bool TryGetLoadedPlugins<TPlugin>(
            IEnumerable<object> source,
            out List<TPlugin> plugins
        )
        {
            plugins = new List<TPlugin>();
            var result = source.Where(x => x.GetType().IsAssignableTo(typeof(TPlugin)));

            if (result != null && result.Any())
            {
                plugins = result.Cast<TPlugin>().ToList();
                return true;
            }

            return false;
        }

        private async Task<CompositePluginCatalog> InitialzeCatalogsAsync()
        {
            if (
                isTypePluginCatalogsStale
                || isAssemblyPluginCatalogsStale
                || aggregateCatalog == null
            )
            {
                aggregateCatalog = new CompositePluginCatalog();

                var machineCatalog = CatalogHelper.CreateFolderPluginCatalog(
                    fsService,
                    FileSystemContext.Machine,
                    "/pluginBuilders"
                );
                var userCatalog = CatalogHelper.CreateFolderPluginCatalog(
                    fsService,
                    FileSystemContext.User,
                    "/pluginBuilders"
                );
                var localCatalog = CatalogHelper.CreateFolderPluginCatalog(
                    fsService,
                    FileSystemContext.Local,
                    "/pluginBuilders"
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

                foreach (var kvp in typePluginCatalogs)
                {
                    aggregateCatalog.AddCatalog(kvp.Value);
                }

                foreach (var kvp in assemblyPluginCatalogs)
                {
                    aggregateCatalog.AddCatalog(kvp.Value);
                }
            }

            await aggregateCatalog.Initialize();

            isTypePluginCatalogsStale = false;
            isAssemblyPluginCatalogsStale = false;

            return aggregateCatalog;
        }

        private void InitializePlugin(
            object concretePlugin,
            Weikio.PluginFramework.Abstractions.Plugin? abstractPlugin
        )
        {
            if (abstractPlugin != null && concretePlugin is PluginBase basePlugin)
            {
                logger.LogInformation(
                    "Initializing plugin {0} v{1}",
                    abstractPlugin.Name,
                    abstractPlugin.Version
                );

                basePlugin.Name = abstractPlugin.Name;
                basePlugin.Version = abstractPlugin.Version;
            }
        }
    }
}
