using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using xSdk.Extensions.IO;
using xSdk.Extensions.Plugin;
using xSdk.Hosting;
using xSdk.Plugins.Authentication;
using xSdk.Shared;

namespace xSdk.Plugins.Documentation
{
    internal sealed class DocumentationPlugin : WebHostPluginBase
    {
        private Dictionary<string, OpenApiInfo> apiInfos = new Dictionary<string, OpenApiInfo>();

        public override void ConfigureServices(
            WebHostBuilderContext context,
            IServiceCollection services
        )
        {
            var authPlugin = SlimHost.Instance.PluginSystem.GetPlugin<AuthenticationPlugin>();
            var pluginBuilders =
                SlimHost.Instance.PluginSystem.GetPlugins<IDocumentationPluginBuilder>();

            foreach (var plugin in pluginBuilders)
            {
                var info = new OpenApiInfo();
                plugin.ConfigureApiDescriptions(info);
                apiInfos.AddOrNew(info.Version, info);
            }

            services.AddSwaggerGen(
                (setup) =>
                {
                    Logger.Info("Configure Swagger Document Generator");
                    foreach (var apiInfo in apiInfos)
                    {
                        setup.SwaggerDoc(apiInfo.Key, apiInfo.Value);
                    }

                    if (authPlugin != null)
                    {
                        setup.AddSecurityDefinition(
                            AuthenticationDefaults.ApiKeyAuth.Name,
                            new OpenApiSecurityScheme()
                            {
                                Description = "API Key Authentication",
                                Type = SecuritySchemeType.ApiKey,
                                In = ParameterLocation.Header,
                                Name = AuthenticationDefaults.ApiKeyAuth.InHeader.Header,
                            }
                        );

                        setup.OperationFilter<AuthorizeCheckOperationFilter>();
                    }

                    setup.OperationFilter<RemoveVersionParameterFilter>();
                    setup.DocumentFilter<ModifyPathsDocumentFilter>();
                    setup.EnableAnnotations();

                    Logger.Trace("Add Docu Infos from current Assembly");
                    LoadXmlDocumentations(setup);

                    SlimHost.Instance.PluginSystem.Invoke<IDocumentationPluginBuilder>(x =>
                        x.ConfigureSwagger(setup)
                    );
                }
            );
        }

        public override void Configure(WebHostBuilderContext context, IApplicationBuilder app)
        {
            var plugins = SlimHost.Instance.PluginSystem.GetPlugins();
            var authPlugin = SlimHost.Instance.PluginSystem.GetPlugin<AuthenticationPlugin>();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            // see also https://cpratt.co/customizing-swagger-ui-in-asp-net-core/
            app.UseSwagger(setup =>
                {
                    setup.SerializeAsV2 = true;
                })
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                .UseSwaggerUI(setup =>
                {
                    var docSetup = SlimHost.Instance.VariableSystem.GetSetup<DocumentationSetup>();

                    setup.RoutePrefix = docSetup.RoutePrefix;
                    foreach (var apiInfo in apiInfos)
                    {
                        setup.SwaggerEndpoint(
                            $"/swagger/{apiInfo.Key}/swagger.json",
                            $"{apiInfo.Value.Title} {apiInfo.Key}"
                        );
                    }

                    SlimHost.Instance.PluginSystem.Invoke<IDocumentationPluginBuilder>(x =>
                        x.ConfigureSwaggerUi(setup)
                    );
                });
        }

        private void LoadXmlDocumentations(SwaggerGenOptions setup)
        {
            // Set the comments path for the Swagger JSON and UI.
            var executionFolder = FileSystemHelper.GetExecutingFolder();
            var xmlFiles = Directory.GetFiles(executionFolder, "*.xml");
            foreach (var xmlFile in xmlFiles)
            {
                FileInfo fileInfo = new FileInfo(xmlFile);
                Logger.Debug("Load Documenation from file '{0}'", fileInfo.Name);
                setup.IncludeXmlComments(xmlFile);
            }
        }
    }
}
