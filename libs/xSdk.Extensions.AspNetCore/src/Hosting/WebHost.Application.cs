using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace xSdk.Hosting
{
    public static partial class WebHost
    {
        private static void ConfigureApplicationWithContext(
            WebHostBuilderContext context,
            IApplicationBuilder app
        )
        {
            Logger.Info("Configuring application services");

            var plugins = SlimHost.Instance.PluginSystem.GetPlugins<WebHostPluginBase>();

            foreach (var plugin in plugins)
            {
                plugin.ConfigureDefaults(context, app);

                plugin.Configure(context, app);
            }

            app.UseEndpoints(builder =>
            {
                foreach (var plugin in plugins)
                {
                    plugin.ConfigureEndpoint(builder);
                }
            });
        }
    }
}
