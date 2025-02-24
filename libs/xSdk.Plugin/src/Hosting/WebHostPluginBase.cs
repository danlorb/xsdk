using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using xSdk.Extensions.Plugin;
using xSdk.Extensions.Variable;

namespace xSdk.Hosting
{
    public class WebHostPluginBase : PluginDescription
    {
        protected ILogger Logger { get; } = LogManager.GetCurrentClassLogger();

        public virtual void ConfigureServices(
            WebHostBuilderContext context,
            IServiceCollection services
        ) { }

        public virtual void ConfigureDefaults(
            WebHostBuilderContext context,
            IApplicationBuilder app
        )
        {
            Logger.Debug("Load Environtment Setup");
            var envSetup = app
                .ApplicationServices.GetRequiredService<IVariableService>()
                .GetSetup<IEnvironmentSetup>();

            if (envSetup != null && envSetup.Stage == Stage.Development)
            {
                app.UseDeveloperExceptionPage();
            }

            Logger.Debug("Enable HTTPS Redirection");
            app.UseHttpsRedirection();

            app.UseRouting();
        }

        public virtual void Configure(WebHostBuilderContext context, IApplicationBuilder app) { }

        public virtual void ConfigureEndpoint(IEndpointRouteBuilder builder) { }
    }
}
