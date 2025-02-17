using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using xSdk.Extensions.IO;
using xSdk.Extensions.Plugin;
using xSdk.Extensions.Variable;

namespace xSdk.Hosting
{
    internal sealed partial class WebHostPlugin : PluginBase, IWebHostBuilderPluginConfig
    {
        public void ConfigureWebHost(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureWebHostDefaults(webHostBuilder =>
            {
                Logger.Debug("Configuring WebHostBuilder");

                var envSetup = SlimHost.Instance.VariableSystem.GetSetup<EnvironmentSetup>();
                var stage = envSetup.Stage;

                webHostBuilder
                    // Set the Content Root
                    .UseContentRoot(GetContentRoot(envSetup))
                    // Set the Environment
                    .UseEnvironment(stage.ToString())
                    // Enable detailed Errors if in Development Mode
                    .UseSetting(
                        WebHostDefaults.DetailedErrorsKey,
                        (stage == Stage.Development).ToString()
                    )
                    .ConfigureServices(
                        (context, services) =>
                        {
                            Host.ConfigureHostDefaultServices(services);
                            ConfigureWebHostServices(context, services);
                        }
                    )
                    // Load Middlewares
                    .Configure((context, appBuilder) => ConfigureApplication(context, appBuilder))
                    // Configure Kestrel
                    .UseKestrel(ConfigureKestrel);

                if (stage == Stage.Development)
                    webHostBuilder.CaptureStartupErrors(true);
            });
        }

        private string GetContentRoot(EnvironmentSetup envSetup)
        {
            Logger.Debug(envSetup.IsDemo ? "Demo Mode" : "Production Mode");
            Logger.Debug("Try to get Content Root");

            var root = envSetup.ContentRoot;
            if (envSetup.IsDemo)
            {
                return FileSystemHelper.GetExecutingFolder();
            }

            if (!Directory.Exists(root))
            {
                try
                {
                    Logger.Trace("Content root does not exist, creating it");
                    Directory.CreateDirectory(root);
                }
                catch
                {
                    // Only catch, nothing to tell
                }
            }
            return Path.GetFullPath(root);
        }
    }
}
