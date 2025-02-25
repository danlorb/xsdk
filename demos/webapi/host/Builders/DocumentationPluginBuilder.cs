using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using xSdk.Extensions.Plugin;
using xSdk.Plugins.Documentation;

namespace xSdk.Demos.Builders
{
    public class DocumentationPluginBuilder : PluginBuilderBase, IDocumentationPluginBuilder
    {
        public void ConfigureApiDescriptions(Dictionary<string, OpenApiInfo> descriptions)
        {
            descriptions
                .Add("v1", new OpenApiInfo
                {
                    Title = "Sample API",
                    Version = "v1"
                });

            descriptions
                .Add("v2", new OpenApiInfo
                {
                    Title = "Sample API TEst",
                    Version = "v2"
                });
        }

        public void ConfigureSwagger(SwaggerGenOptions options) { }

        public void ConfigureSwaggerUi(SwaggerUIOptions options) { }
    }
}
