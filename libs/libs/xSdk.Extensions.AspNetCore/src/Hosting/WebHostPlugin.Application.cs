using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using xSdk.Extensions.Plugin;
using xSdk.Extensions.Variable;
using xSdk.Extensions.Web;

namespace xSdk.Hosting
{
    internal sealed partial class WebHostPlugin
    {
        private void ConfigureApplication(WebHostBuilderContext context, IApplicationBuilder app)
        {
            Logger.Info("Configuring application services");

            Logger.Debug("Load Environtment Setup");
            var envSetup = app
                .ApplicationServices.GetRequiredService<IVariableService>()
                .GetSetup<EnvironmentSetup>();

            //Logger.Debug("Enable HTTPS Redirection");
            //app.UseHttpsRedirection();

            // Possible do Static Files routing here
            SlimHost.Instance.PluginSystem.Invoke<IApplicationPreparePluginConfig>(x =>
                x.Configure(app)
            );

            app.UseRouting();

            SlimHost.Instance.PluginSystem.Invoke<IApplicationPluginConfig>(x => x.Configure(app));

            app.UseEndpoints(builder =>
                SlimHost.Instance.PluginSystem.Invoke<IEndpointPluginConfig>(x =>
                    x.ConfigureEndpoint(builder)
                )
            );
        }
    }
}
