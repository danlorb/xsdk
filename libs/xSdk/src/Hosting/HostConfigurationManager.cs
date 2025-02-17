using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Filters;
using xSdk.Extensions.IO;
using xSdk.Extensions.Variable;

namespace xSdk.Hosting
{
    public static class HostConfigurationManager
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public static void LoadHostConfiguration(IConfigurationBuilder builder)
        {
            logger.Info("Try to load Machine Configuration");

            logger.Trace("Clear all Configuration Providers and load our own Providers");
            builder.Sources.Clear();

            builder.AddEnvironmentVariables(prefix: "DOTNET_");
            builder.AddEnvironmentVariables(prefix: "ASPNET_");
            builder.AddEnvironmentVariables(
                prefix: $"{SlimHost.Instance.AppPrefix.ToUpperInvariant()}_"
            );
        }

        internal static void LoadAppConfiguration(IConfigurationBuilder builder)
        {
            LoadAppConfiguration(null, builder);
        }

        public static void LoadAppConfiguration(
            HostBuilderContext? context,
            IConfigurationBuilder builder
        )
        {
            logger.Info("Try to load Application Configuration");

            var fileSystemService = new FileSystemService();
            var root = fileSystemService
                .RequestFileSystemAsync(FileSystemContext.Machine)
                .GetAwaiter()
                .GetResult();

            var configFolder = FileSystemHelper.CreateSpecificDataFolder(root, "/config");

            var configFile = GetConfigFile(configFolder);
            LoadConfigurationFile(builder, configFile, false);

            if (context != null)
            {
                configFile = GetConfigFile(
                    configFolder,
                    context.HostingEnvironment.EnvironmentName
                );
                LoadConfigurationFile(builder, configFile, true);
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                logger.Trace("Try load Config from Container");
                if (Directory.Exists("/var/run/configs"))
                {
                    builder.AddKeyPerFile("/var/run/configs", true);
                }
            }
        }

        private static void LoadConfigurationFile(
            IConfigurationBuilder builder,
            string? file,
            bool reloadOnChange = false
        )
        {
            if (!string.IsNullOrEmpty(file) && File.Exists(file))
            {
                logger.Info("Try to load Configuration from File '{0}'", file);
                logger.Trace("Configuration File exists. Load it!");
                builder.AddJsonFile(file, true, reloadOnChange);
            }
            else
            {
                logger.Info("Configuration File not exists. Nothing to do.");
            }
        }

        private static string? GetConfigFile(string configFolder, string? envName = null)
        {
            var logPostFix = "";

            var configFileName = $"appsettings.json".ToLower();
            if (!string.IsNullOrEmpty(envName))
            {
                configFileName = $"appsettings.{envName}.json".ToLower();
                logPostFix = $" for Environment '{envName}'";
            }

            logger.Info(
                "Try to determine configuration file in folder '{0}'{1}",
                configFolder,
                logPostFix
            );
            var configFile = Path.Combine(configFolder, configFileName);

            if (!File.Exists(configFile))
            {
                logger.Trace("Configuration file could not found!");
                configFolder = FileSystemHelper.GetExecutingFolder();

                logger.Info(
                    "Last try! Try to load configuration file from Visual Studio project folder '{0}'{1}",
                    configFolder,
                    logPostFix
                );
                configFile = Path.Combine(configFolder, configFileName);
            }

            if (!File.Exists(configFile))
            {
                logger.Trace("Give up! Configuration file could not found.");
                return null;
            }
            else
            {
                logger.Trace("Success! Configuration file found.");
                return configFile;
            }
        }
    }
}
