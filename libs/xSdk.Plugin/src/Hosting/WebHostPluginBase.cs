using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using xSdk.Extensions.Plugin;
using xSdk.Extensions.Variable;

namespace xSdk.Hosting
{
    public class WebHostPluginBase : PluginDescription, IPlugin
    {
        public virtual void ConfigureServices(WebHostBuilderContext context, IServiceCollection services) { }

        public virtual void ConfigureDefaults(WebHostBuilderContext context, IApplicationBuilder app) { }

        public virtual void Configure(WebHostBuilderContext context, IApplicationBuilder app) { }

        public virtual void ConfigureEndpoint(IEndpointRouteBuilder builder) { }
    }
}
