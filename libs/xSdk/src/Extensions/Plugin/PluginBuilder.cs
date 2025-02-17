using System.Reflection;
using Weikio.PluginFramework.Abstractions;

namespace xSdk.Extensions.Plugin
{
    public class PluginBuilder
    {
        internal List<IPluginCatalog> _catalogs = new();

        public void AddPlugin(Type pluginType)
        {
            _catalogs.Add(CatalogHelper.CreateTypeCatalog(pluginType));
        }

        public void AddPlugin<TPlugin>()
            where TPlugin : IPlugin
        {
            AddPlugin(typeof(TPlugin));
        }

        public void AddPluginsFrom<TSource>()
        {
            var assembly = typeof(TSource).Assembly;
            AddPluginsFrom(assembly);
        }

        public void AddPluginsFrom(Assembly sourceAssembly)
        {
            _catalogs.Add(CatalogHelper.CreateAssemblyCatalog(sourceAssembly));
        }
    }
}
