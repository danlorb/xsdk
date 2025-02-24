using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using xSdk.Extensions.Plugin;
using xSdk.Hosting;
using xSdk.Plugins.Documentation;

namespace xSdk.Extensions.Documentation.Fakes
{
    internal class DocumentationPluginBuilderFake : PluginBuilderBase, IDocumentationPluginBuilder
    {
        public void ConfigureApiDescriptions(OpenApiInfo info)
        {
            info.Title = "Sample API";
        }

        public void ConfigureSwagger(SwaggerGenOptions options) { }

        public void ConfigureSwaggerUi(SwaggerUIOptions options) { }
    }
}
