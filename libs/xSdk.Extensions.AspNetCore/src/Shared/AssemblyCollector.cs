using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using xSdk.Data;
using xSdk.Hosting;

namespace xSdk.Shared
{
    internal static class AssemblyCollector
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        internal static List<Assembly> Collect()
        {
            Logger.Info("Collect loaded Assemblies");

            var assemblies = new List<Assembly>();

            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                Logger.Debug("Add Entry Assembly");
                AddAssembly(assemblies, entryAssembly);
            }

            Logger.Debug("Add xSdk Assembly");
            AddAssembly(assemblies, typeof(TestHost).Assembly);

            Logger.Debug("Add xSdk.Data Assembly");
            AddAssembly(assemblies, typeof(FakeGenerator).Assembly);

            Logger.Debug("Add xSdk.Plugin Assembly");
            AddAssembly(assemblies, typeof(SdkException).Assembly);

            Logger.Debug("Add this Assembly");
            AddAssembly(assemblies, typeof(AssemblyCollector).Assembly);

            Logger.Debug("Add assemblies from loaded plugins");
            var plugins = SlimHost.Instance.PluginSystem.GetPlugins();
            foreach (var plugin in plugins)
            {
                var assembly = plugin.GetType().Assembly;
                AddAssembly(assemblies, assembly);
            }

            return assemblies;
        }

        private static void AddAssembly(List<Assembly> assemblies, Assembly assembly)
        {
            var assemblyNames = assemblies.Select(x => x.FullName).Where(x => x != null);
            var name = assembly.FullName;
            if (!string.IsNullOrEmpty(name) && !assemblyNames.Contains(name))
            {
                assemblies.Add(assembly);
            }
        }
    }
}
