using System.Diagnostics;
using System.Reflection;
using Asp.Versioning;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using xSdk.Data;
using xSdk.Extensions.Plugin;
using xSdk.Extensions.Variable;
using xSdk.Hosting;

namespace xSdk.Plugins.WebApi
{
    public class WebApiPlugin : WebHostPluginBase
    {
        public override void ConfigureServices(
            WebHostBuilderContext context,
            IServiceCollection services
        )
        {
            Logger.Trace("Load Setups for Web Host Builder");
            services
                // Add Context Accessor
                .AddHttpContextAccessor()
                // Add Problem Details Services
                .AddProblemDetails(_ =>
                {
                    var currentStage = SlimHost
                        .Instance.VariableSystem.GetSetup<EnvironmentSetup>()
                        .Stage;

                    _.IncludeExceptionDetails = (ctx, ex) =>
                    {
                        if (
                            Debugger.IsAttached
                            || currentStage == Stage.Development
                            || currentStage == Stage.Integration
                        )
                            return true;

                        return false;
                    };
                    _.ShouldLogUnhandledException = (ctx, ex, details) =>
                    {
                        return true;
                    };
                })
                // Add Routing
                .AddRouting(_ =>
                {
                    _.LowercaseUrls = true;
                    _.LowercaseQueryStrings = true;
                    _.SuppressCheckForUnhandledSecurityMetadata = false;
                });

            var mvcBuilder = services
                .AddControllers(_ =>
                {
                    _.InputFormatters.Add(new PlainTextFormatter());
                    SlimHost.Instance.PluginSystem.Invoke<IWebApiPluginBuilder>(x =>
                        x.ConfigureMvc(_)
                    );
                })
                // Configure Json
                .AddJsonOptions(_ =>
                {
                    _.JsonSerializerOptions.ConfigureSerializerOptions();
                });

            // Enable Versioning
            services.AddApiVersioning(_ =>
            {
                // Add the headers "api-supported-versions" and "api-deprecated-versions"
                // This is better for discoverability
                _.ReportApiVersions = true;

                // AssumeDefaultVersionWhenUnspecified should only be enabled when supporting legacy services that did not previously
                // support API versioning. Forcing existing clients to specify an explicit API version for an
                // existing service introduces a breaking change. Conceptually, clients in this situation are
                // bound to some API version of a service, but they don't know what it is and never explicit request it.
                _.AssumeDefaultVersionWhenUnspecified = true;
                _.DefaultApiVersion = new ApiVersion(1, 0);

                // Defines how an API version is read from the current HTTP request
                _.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("api-version")
                );
            });

            services.AddEndpointsApiExplorer();

            LoadApplicationParts(mvcBuilder);
        }

        public override void Configure(WebHostBuilderContext context, IApplicationBuilder app)
        {
            app.UseStatusCodePages().UseProblemDetails();
        }

        public override void ConfigureEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapControllers();
        }

        private void LoadApplicationParts(IMvcBuilder builder)
        {
            Logger.Info("Add Application Parts");

            var assemblies = new List<Assembly>();

            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                Logger.Debug("Add Application Part from Entry Assembly");
                assemblies.Add(entryAssembly);
            }

            Logger.Debug("Add Application Part from this Assembly");
            assemblies.Add(GetType().Assembly);

            Logger.Info("Load allParts from other Assemblies");
            var plugins = SlimHost.Instance.PluginSystem.GetPlugins();
            foreach (var plugin in plugins)
            {
                var assembly = plugin.GetType().Assembly;
                assemblies.Add(assembly);
            }

            var assemblyNames = new List<string>();
            foreach (var assembly in assemblies)
            {
                var name = assembly.FullName;
                if (!assemblyNames.Contains(name))
                {
                    builder.AddApplicationPart(assembly);
                    assemblyNames.Add(name);
                }
            }
        }
    }
}
