using NLog;
using System.Reflection;
using xSdk.Data;
using xSdk.Extensions.IO;
using xSdk.Hosting;

namespace xSdk.Shared
{
    internal static class AssemblyCollector
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static List<Assembly> assemblies;

        internal static List<Assembly> Collect()
        {
            Logger.Info("Collect loaded Assemblies");

            if (assemblies == null || assemblies.Count == 0)
            {
                assemblies = new List<Assembly>();

                Logger.Debug("Add referencedAssemblies Assemblies");
                AddReferencedAssemblies(assemblies);

                Logger.Debug("Add assemblies from loaded plugins");
                var plugins = SlimHost.Instance.PluginSystem.GetPlugins();
                foreach (var plugin in plugins)
                {
                    var assembly = plugin.GetType().Assembly;
                    AddAssembly(assemblies, assembly);
                }
            }

            return assemblies;
        }

        private static void AddReferencedAssemblies(List<Assembly> assemblies)
        {
            var referencedAssemblies = LoadReferencedAssemblies();

            referencedAssemblies
                .ToList()
                .ForEach(x => AddAssembly(assemblies, x));
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

        private static IEnumerable<Assembly> LoadReferencedAssemblies()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            var executingFolder = FileSystemHelper.GetExecutingFolder();
            var assemblyFiles = new DirectoryInfo(executingFolder)
                .EnumerateFiles("*.dll", SearchOption.TopDirectoryOnly)
                .Where(x => !IsBlacklisted(x.Name));

            return assemblyFiles
                .Select(x => loadedAssemblies.FirstOrDefault(y => x.FullName == y.Location)!)
                .Where(x => x != null)
                .Distinct();
        }

        private static bool IsBlacklisted(string name)
        {
            var pattern = new string[]
            {
                "Asp",
                "AspNetCore",
                "AutoMapper",
                "Bogus",
                "CloudNative",
                "FluentValidation",
                "Google",
                "Grpc",
                "Handlebars",
                "Hellang",
                "LiteDB",
                "MicroElements",
                "Microsoft",
                "MongoDB",
                "NLog",
                "NWebsec",
                "Newtonsoft",
                "OpenTelemetry",
                "RestSharp",
                "SemanticVersioning",
                "Sewer56",
                "Spectre",
                "Swashbuckle",
                "System",
                "VaultSharp",
                "Weikio",
                "YamlDotNet",
                "Zio",
                "netcore",
                "netstandard",
            };

            return pattern.Any(x => name.StartsWith(x));
        }
    }
}
